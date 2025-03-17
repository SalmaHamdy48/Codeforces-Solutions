const button = document.getElementById("button");

button.addEventListener("click", function (event) {
  event.preventDefault();

  const number = document.getElementById("number").value;
  const text = document.getElementById("textChoice").value;
  const tagType = document.getElementById("dsChoice").value;
  const output = document.getElementById("output");

  
  output.innerHTML = "";

  
  for (let i = 0; i < number; i++) {
    const box = document.createElement(tagType);
    box.classList.add("box");
    box.textContent = text;
    output.appendChild(box);
  }
});
