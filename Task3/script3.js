let formInput = document.querySelector("#form");
formInput.addEventListener("input", function () {
    event.preventDefault();
    const value = document.getElementById("trans").value;

    const exchange = 15.6;
    const result = value * exchange;
    document.getElementById("result").innerHTML = `{${value}} USD = {${result}} Egyptian Pounds`;
    
});