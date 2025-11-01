# Ruta base de tu proyecto
$ProjectPath = "E:\GIT\BackEnd-upch"
$PublishPath = "$ProjectPath\publish"
$LambdaZip = "$ProjectPath\lambda.zip"
$LambdaFunctionName = "CarsServiceLambda"

# 1️⃣ Publicar la aplicación
Write-Host "Publicando proyecto..."
dotnet publish $ProjectPath -c Release -o $PublishPath

# 2️⃣ Comprimir la carpeta publish en lambda.zip
Write-Host "Creando zip..."
if (Test-Path $LambdaZip) { Remove-Item $LambdaZip -Force }
Compress-Archive -Path "$PublishPath\*" -DestinationPath $LambdaZip -Force

# 3️⃣ Subir el ZIP a AWS Lambda
Write-Host "Actualizando función Lambda..."
aws lambda update-function-code --function-name $LambdaFunctionName --zip-file fileb://$LambdaZip

Write-Host "✅ Proceso completado. Lambda actualizado."
