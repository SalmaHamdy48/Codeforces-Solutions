let theNumber = 100020003000;

const result = Number([...new Set([...String(theNumber)])].sort().join(''));
console.log(result); 


// Needed Output
//123