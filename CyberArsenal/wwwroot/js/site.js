// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function iconJump(icon) {
    var maxJump = 6;
    var speed = 2;
    var jumpPos = 0;

    var mode = -1;
    if (mode == -1) {
        mode = 0;
        var intervals = setInterval(function () {
            switch (mode) {
                case 0:
                    jumpPos = jumpPos + 1 * speed;
                    icon.style.top = -jumpPos + "px";
                    if (jumpPos >= maxJump)
                        mode = 1;
                    break;
                case 1:
                    jumpPos = jumpPos - 1 * speed;
                    icon.style.top = -jumpPos + "px";
                    if (jumpPos <= 0) {
                        mode = 2;
                        icon.style.top = 0 + "px";
                    }
                    break;
                case 2:
                    clearInterval(intervals);
            }
        }, 20);
    }
}