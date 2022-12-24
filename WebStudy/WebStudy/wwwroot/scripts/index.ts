/// <reference path="Canvas.ts" />

function execute() {
    canvas = new Canvas(document.querySelector("#graphics") as HTMLCanvasElement);
    canvas.execute();
}

let canvas: Canvas;