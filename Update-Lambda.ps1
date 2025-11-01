# -----------------------------------------------
# Script avanzado para publicar proyecto .NET y actualizar Lambda
# -----------------------------------------------

$ProjectPath = "E:\GIT\BackEnd-upch"
$PublishPath = "$ProjectPath\publish"
$LambdaZip = "$ProjectPath\lambda.zip"
$LambdaFunctionName = "CarsServiceLambda"
$BackupPath = "$ProjectPath\backup"
$DryRun = $false  # Cambia a $true para simular sin subir a Lambda

# -----------------------------
# Crear carpeta de backup si no existe
# -----------------------------
if (-Not (Test-Path $BackupPath)) {
    New-Item -ItemType Directory -Path $BackupPath | Out-Null
}

# -----------------------------
# 1️⃣ Limpiar publish y ZIP previo
# -----------------------------
Write-Host "🔹 Limpiando carpetas previas..."
if (Test-Path $PublishPath) { Remove-Item $PublishPath -Recurse -Force }

if (Test-Path $LambdaZip) {
    $BackupZip = "$BackupPath\lambda_backup_$(Get-Date -Format 'yyyyMMdd_HHmmss').zip"
    Write-Host "Haciendo backup del ZIP previo a $BackupZip..."
    Copy-Item $LambdaZip $BackupZip -Force
    Remove-Item $LambdaZip -Force
}

# -----------------------------
# 2️⃣ Limpiar y publicar proyecto
# -----------------------------
Write-Host "🔹 Limpiando proyecto..."
dotnet clean $ProjectPath
if ($LASTEXITCODE -ne 0) { throw "❌ Error en dotnet clean." }

Write-Host "🔹 Publicando proyecto..."
dotnet publish $ProjectPath -c Release -o $PublishPath -v:minimal
if ($LASTEXITCODE -ne 0) { throw "❌ Error en dotnet publish. Verifica errores de compilación." }

# -----------------------------
# 3️⃣ Verificar que haya archivos para zip
# -----------------------------
$files = Get-ChildItem -Path $PublishPath -Recurse
if ($files.Count -eq 0) { throw "❌ No hay archivos en la carpeta publish. Nada que comprimir." }

# -----------------------------
# 4️⃣ Comprimir en ZIP
# -----------------------------
Write-Host "🔹 Creando ZIP..."
Compress-Archive -Path "$PublishPath\*" -DestinationPath $LambdaZip -Force
if (-Not (Test-Path $LambdaZip)) { throw "❌ No se pudo crear el ZIP." }

Write-Host "📦 ZIP creado con tamaño: $([math]::Round((Get-Item $LambdaZip).Length / 1MB, 2)) MB"

# -----------------------------
# 5️⃣ Subir a Lambda
# -----------------------------
if ($DryRun) {
    Write-Host "🟡 Dry Run activado. No se subirá a Lambda."
} else {
    Write-Host "🔹 Actualizando función Lambda: $LambdaFunctionName..."
    aws lambda update-function-code --function-name $LambdaFunctionName --zip-file fileb://$LambdaZip
    if ($LASTEXITCODE -ne 0) { throw "❌ Error al actualizar Lambda." }

    # -----------------------------
    # 6️⃣ Verificar actualización
    # -----------------------------
    $lambdaInfo = aws lambda get-function --function-name $LambdaFunctionName | ConvertFrom-Json
    Write-Host "✅ Lambda actualizado correctamente."
    Write-Host "Última modificación: $($lambdaInfo.Configuration.LastModified)"
    Write-Host "Tamaño del ZIP en Lambda: $([math]::Round($lambdaInfo.Configuration.CodeSize / 1MB, 2)) MB"
}

# -----------------------------
# 7️⃣ Finalización
# -----------------------------
Write-Host "🎉 Deploy completado."
