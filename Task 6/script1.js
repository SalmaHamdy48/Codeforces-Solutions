let chars = ["A", "B", "C", 20, "D", "E", 10, 15, 6];

let numbers = chars.filter(el => typeof el === "number");
let letters = chars.filter(el => typeof el === "string");

let replacement = letters.slice(0, numbers.length);// numbers == letters to be replaced
let result = [...replacement, ...letters];

console.log(result);

// Needed Output
//['A', 'B', 'C', 'D', 'A', 'B', 'C', 'D', 'E']
