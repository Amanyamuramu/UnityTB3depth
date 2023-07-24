#!/bin/bash

# 引数が指定されているか確認
if [ $# -eq 0 ]; then
    echo "Usage: $0 SOURCE_DIR"
    exit 1
fi

# 変換元ディレクトリを引数から取得
SOURCE_DIR="$1"

# 変換元ディレクトリの存在を確認
if [ ! -d "$SOURCE_DIR" ]; then
    echo "Error: $SOURCE_DIR is not a valid directory."
    exit 1
fi

# 変換後のPDFファイルを保存するディレクトリを指定
DESTINATION_DIR="C:\Users\tetur\Documents\unity\Ros2DepthCameraVR\PictureData\ExportPdf"

# PNGファイルをPDFに変換
for file in "$SOURCE_DIR"/*.png; do
    if [ -f "$file" ]; then
        filename=$(basename "$file")
        extension="${filename##*.}"
        filename="${filename%.*}"
        output_file="$DESTINATION_DIR/$filename.pdf"

        # ImageMagickを使ってPNGからPDFに変換
        convert "$file" "$output_file"
        echo "Converted: $filename.png -> $filename.pdf"
    fi
done
