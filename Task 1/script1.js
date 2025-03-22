let chosen = 1;

let myFriends = [
  { title: "Osama", age: 39, available: true, skills: ["HTML", "CSS"] },
  { title: "Ahmed", age: 25, available: false, skills: ["Python", "Django"] },
  { title: "Sayed", age: 33, available: true, skills: ["PHP", "Laravel"] },
];

if(chosen === 1) {
  console.log(` "${myFriends[0].title}" 
  ${myFriends[0].age} 
  "${myFriends[0].available}"
  "${myFriends[0].skills[1]}"`);
}

else if(chosen === 2) {
  console.log(` "${myFriends[1].title}" 
  ${myFriends[1].age} 
  "${myFriends[1].available ? "Available" : "Not Available"}"
  "${myFriends[1].skills[1]}"`);
}

else{
  console.log(` "${myFriends[2].title}" 
  ${myFriends[2].age} 
  "${myFriends[2].available}"
  "${myFriends[2].skills[1]}"`);
}
