function fillProgress(cpuValue, gpuValue, ramValue, storageValue) {
    var cpu = document.getElementById("progress-bar-cpu");
    var gpu = document.getElementById("progress-bar-gpu");
    var ram = document.getElementById("progress-bar-ram");
    var storage = document.getElementById("progress-bar-storage");

    let components = [cpu, gpu, ram, storage];
    let values = [cpuValue, gpuValue, ramValue, storageValue];

    components.forEach(function (item, index, array) {
        var child = item.childNodes[1];

        if (values[index] > 100) {
            var percent = values[index];
            if (percent > 100) {
                percent = 100;
            }
            item.style.width = String(percent) + "%";
        }

        child.style.width = String(values[index]) + "%";
        child.innerHTML = String(values[index]) + "%";
        if (values[index] >= "60") {
            child.classList.add("my-bg-orange");
        }
        if (values[index] >= "80") {
            child.classList.add("my-bg-green");
        }
        if (values[index] > "100") {
            child.classList.add("my-bg-blue");
        }
    })
}