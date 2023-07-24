# 引数が指定されているか確認
param (
    [Parameter(Mandatory=$true)]
    [string]$SourceDir
)

# 変換元ディレクトリの存在を確認
if (!(Test-Path -Path $SourceDir -PathType Container)) {
    Write-Error "Error: $SourceDir is not a valid directory."
    exit 1
}

# 変換後のPDFファイルを保存するディレクトリを指定
$DestinationDir = "C:\Users\tetur\Documents\unity\Ros2DepthCameraVR\PictureData\ExportPdf"

# ImageMagickがインストールされていることを確認（このパスはImageMagickのインストール先による）
$convertPath = "C:\Program Files\ImageMagick-7.0.8-Q16\convert.exe"
if (!(Test-Path -Path $convertPath)) {
    Write-Error "Error: ImageMagick not found. Please install ImageMagick."
    exit 1
}

# PNGファイルをPDFに変換
Get-ChildItem -Path $SourceDir -Filter "*.png" | ForEach-Object {
    $filename = $_.BaseName
    $output_file = Join-Path -Path $DestinationDir -ChildPath "$filename.pdf"

    # ImageMagickを使ってPNGからPDFに変換
    & $convertPath "$($_.FullName)" "$output_file"
    Write-Output "Converted: $filename.png -> $filename.pdf"
}
