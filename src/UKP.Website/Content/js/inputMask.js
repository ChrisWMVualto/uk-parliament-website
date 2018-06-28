$(function () {
    initInputMask();
});

function initInputMask() {
    var startDownload = document.getElementById("downloadStartTime");
    var endDownload = document.getElementById("downloadEndTime");

    if (startDownload != null && endDownload != null) {
        startDownload.addEventListener("keydown", inputMask);
        startDownload.addEventListener("input", mobileMask);
        endDownload.addEventListener("keydown", inputMask);
        endDownload.addEventListener("input", mobileMask);
    }
}

function initShareInputMask() {
    var startShare = document.getElementById("shareStartTime");
    var endShare = document.getElementById("shareEndTime");

    if (startShare != null && endShare != null) {
        startShare.addEventListener("keydown", inputMask);
        startShare.addEventListener("input", mobileMask);
        endShare.addEventListener("keydown", inputMask);
        endShare.addEventListener("input", mobileMask);
    }
}

function mobileMask(e) {
    var mask = "00:00:00";
    var beforeInput = e.target.dataset.lastInput;
    if (androidInput) {
        var to = this.selectionStart;
        var from = this.selectionEnd;
        var input;
        if (to === from) {
            input = e.target.value.substring(to - 1, from);
            to--;
        } else input = e.target.value.substring(to, from);
        if (beforeInput !== e.target.value) {
            var key = parseInt(input);
            if (isNaN(key) || to === 8) {
                e.target.value = beforeInput;
                return;
            }
            if (beforeInput.substring(to, to + 1) === ":") {
                to++;
                from++;
            }
            if (e.target.value.substring(to + 1, to + 2) === ":") {
                to++;
                from++;
            }

            if (to === 0 && key > 2) {
                key = 2;
                if (parseInt(e.target.value.substr(to + 1, to + 2)) > 3 && key > 1) {
                    key = 1;
                }
            }
            if (to === 1 && e.target.value.substr(0, 1) === "2" && key > 3) key = 3;
            if ((to === 3 || to === 6 || to === 2 || to === 5) && key > 5) key = 5;


            e.target.value = beforeInput.substring(0, to) + key + beforeInput.substring(from, beforeInput.length);
            e.target.value += mask.substr(e.target.value.length, mask.length);

            beforeInput = e.target.value;
            e.target.dataset.lastInput = beforeInput;
            this.selectionStart = to + 1;
            this.selectionEnd = to + 1;
        }
        androidInput = false;
    }
}

function inputMask(e) {
    var mask = "00:00:00";
    var text = e.target.value;
    var to = this.selectionStart;
    var from = this.selectionEnd;
    var input = "";

    if (e.keyCode === 229) {
        androidInput = true;
        return;
    }

    //Arrow Keys
    if (e.keyCode < 41 && e.keyCode > 36) return;
    e.preventDefault();

    //Backspace
    if (e.keyCode === 8) {
        if (to === from) to--;
        input = text.substring(0, to) + mask.substring(to, from) + text.substring(from, text.length);
        ApplyMask(e.target, input, mask, to);
    }

    //Delete
    if (e.keyCode === 46) {
        if (to === from && e.keyCode === 46) from++;
        input = text.substring(0, to) + mask.substring(to, from) + text.substring(from, text.length);
        ApplyMask(e.target, input, mask, from);
    }

    //Numbers
    var key = parseInt(e.key);
    if (isNaN(key)) return;
    if (to === 8) return;

    if (to === 0 && key > 2) {
        key = 2;
    }
    if (parseInt(text.substr(to + 1, to + 2)) > 3 && key > 1) {
        key = 1;
    }
    if (to === 1 && text.substr(0, 1) === "2" && key > 3) key = 3;
    if ((to === 3 || to === 6 || to === 2 || to === 5) && key > 5) key = 5;

    if (text.substring(to, to + 1) === ":") {
        to++;
        from++;
    }
    if (to === from) {
        input = text.substr(0, to) + key + text.substr(from + 1, text.length);
    } else {
        var replace = key + mask.substr(to + 1, from - 1);
        input = text.substring(0, to) + replace;
        var length = input.length;
        input += text.substr(length, 8);
    }

    ApplyMask(e.target, input, mask, to + 1);
}

function ApplyMask(element, value, mask, cursor) {
    value += mask.substr(value.length, mask.length);
    element.value = value;
    element.selectionStart = cursor;
    element.selectionEnd = element.selectionStart;
    element.dataset.lastInput = element.value;
}

var androidInput = false;