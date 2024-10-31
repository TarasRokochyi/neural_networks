//window.drawing = {
//    context: null,
//
//    startDrawing: function (canvasRef, x, y) {
//        const rect = canvasRef.getBoundingClientRect();
//        const scaleX = canvasRef.width / rect.width;
//        const scaleY = canvasRef.height / rect.height;
//        const canvas = canvasRef;
//
//        this.context = canvas.getContext("2d");
//        this.context.lineWidth = 4; // Set line width to 5 pixels for a bolder line
//        this.context.beginPath();
//        this.context.moveTo((x - rect.left) * scaleX, (y - rect.top) * scaleY);
//    },
//
//    draw: function (canvasRef, x, y) {
//        if (this.context) {
//            const rect = canvasRef.getBoundingClientRect();
//            const scaleX = canvasRef.width / rect.width;
//            const scaleY = canvasRef.height / rect.height;
//
//            this.context.lineTo((x - rect.left) * scaleX, (y - rect.top) * scaleY);
//            this.context.stroke();
//        }
//    },
//
//    stopDrawing: function () {
//        if (this.context) {
//            this.context.closePath();
//            this.context = null;
//        }
//    },
//
//    clearCanvas: function (canvasRef) {
//        const canvas = canvasRef;
//        const context = canvas.getContext("2d");
//        context.clearRect(0, 0, canvas.width, canvas.height);
//    }
//};

window.drawing = {
    context: null,
    canvas: null,
    isDrawing: false,

    startDrawing: function (canvasRef, x, y) {
        const rect = canvasRef.getBoundingClientRect();
        const scaleX = canvasRef.width / rect.width;
        const scaleY = canvasRef.height / rect.height;
        this.canvas = canvasRef;

        this.context = this.canvas.getContext("2d");
        this.context.lineWidth = 1; // Set line width to 5 pixels for a bolder line
        this.isDrawing = true;

        this.context.beginPath();
        this.context.moveTo((x - rect.left) * scaleX, (y - rect.top) * scaleY);
    },

    draw: function (canvasRef, x, y) {
        if (this.isDrawing) {
            const rect = canvasRef.getBoundingClientRect();
            const scaleX = canvasRef.width / rect.width;
            const scaleY = canvasRef.height / rect.height;

            this.context.lineTo((x - rect.left) * scaleX, (y - rect.top) * scaleY);
            this.context.stroke();
        }
    },

    stopDrawing: function () {
        if (this.isDrawing) {
            this.context.closePath();
            this.isDrawing = false;
        }
    },

    clearCanvas: function () {
        if (this.context) {
            this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
        }
    },

    centerAndResizeDrawing: function () {
        if (!this.context || !this.canvas) return;

        // Get the bounding box of the drawn area
        const imageData = this.context.getImageData(0, 0, this.canvas.width, this.canvas.height);
        console.log(imageData);
        let minX = this.canvas.width, minY = this.canvas.height, maxX = 0, maxY = 0;

        for (let y = 0; y < this.canvas.height; y++) {
            for (let x = 0; x < this.canvas.width; x++) {
                const index = (y * this.canvas.width + x) * 4;
                if (imageData.data[index + 3] > 0) { // If pixel is not transparent
                    minX = Math.min(minX, x);
                    minY = Math.min(minY, y);
                    maxX = Math.max(maxX, x);
                    maxY = Math.max(maxY, y);
                }
            }
        }

        // Calculate the width and height of the cropped area
        let cropWidth = maxX - minX + 1;
        let cropHeight = maxY - minY + 1;
        if (cropHeight > cropWidth) {
            minX -= (cropHeight - cropWidth) / 2;
            maxX += (cropHeight - cropWidth) / 2;
            cropWidth = cropHeight;
        }
        else {
            minY -= (cropWidth - cropHeight) / 2;
            maxY += (cropWidth - cropHeight) / 2;
            cropHeight = cropWidth;
        }

        // Extract the image data of the drawn area
        if (cropWidth > 0 && cropHeight > 0) {
            //const croppedImageData = this.context.getImageData(minX, minY, cropWidth, cropHeight);

            //// Clear the canvas
            //this.clearCanvas();

            //// Center and resize the cropped image back to the canvas size
            //const offsetX = (this.canvas.width - cropWidth) / 2;
            //const offsetY = (this.canvas.height - cropHeight) / 2;

            //// Draw the cropped image in the center of the canvas
            ////this.context.putImageData(croppedImageData, offsetX, offsetY);
            //this.context.drawImage(croppedImageData, 0, 0, this.canvas.width, this.canvas.height)

            const tempCanvas = document.createElement('canvas');
            tempCanvas.width = cropWidth;
            tempCanvas.height = cropHeight;
            const tempContext = tempCanvas.getContext('2d');

            // Draw the cropped image onto the temporary canvas
            tempContext.putImageData(this.context.getImageData(minX, minY, cropWidth, cropHeight), 0, 0);

            // Clear the original canvas
            this.clearCanvas();

            // Draw the cropped image in the full size of the original canvas
            this.context.drawImage(tempCanvas, 0, 0, this.canvas.width, this.canvas.height);
        }
    },

    //getImageDataArray: function (canvasRef) {
    //    const context = canvasRef.getContext("2d");
    //    const imageData = context.getImageData(0, 0, canvasRef.width, canvasRef.height);
    //    console.log(imageData)
    //    return Array.from(imageData.data); // Convert imageData.data (Uint8ClampedArray) to a regular array
    //},

    getBinaryImageDataArray: function (canvasRef) {
        const context = canvasRef.getContext("2d");
        const imageData = context.getImageData(0, 0, canvasRef.width, canvasRef.height);
        const binaryArray = [];

        for (let i = 0; i < imageData.data.length; i += 4) {
            // Check the alpha channel (i + 3) to determine transparency
            if (imageData.data[i + 3] > 0) {
                binaryArray.push(1); // Non-transparent pixel
            } else {
                binaryArray.push(0); // Transparent pixel
            }
        }

        return binaryArray; // Return an array of 0s and 1s
    }

};

