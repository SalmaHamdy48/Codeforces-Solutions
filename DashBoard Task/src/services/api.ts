// services/api.ts
import axios from 'axios';

interface Name {
  first: string;
  last: string;
}

interface UserRaw {
  name: Name;
  email: string;
  phone: string;
  picture: {
    thumbnail: string;
  };
  login: {
    username: string;
  };
  registered: {
    date: string;
  };
}

export interface TeamMember {
  id: string;
  name: string;
  position: string;
  department: string;
  email: string;
  phone: string;
  status: 'Full Time' | 'Part Time';
  officeLocation: string;
  teamMates: string[];
  birthday: string;
  hireYear: string;
  address: string;
  avatar: string;
}

const positions = ['Graphics Designer', 'Joomla Developer', 'Human Resource', 'PHP Developer', 'UI UX Designer', 'UX Architect', 'Python Developer', 'Freakers'];
const departments = ['Sales Team', 'Finances', 'Management', 'Engineering', 'Human Resources', 'Customer Success', 'Marketing', 'Product'];

export const fetchTeamMembers = async (page: number, results: number): Promise<TeamMember[]> => {
  try {
    const response = await axios.get<{ results: UserRaw[] }>(`https://randomuser.me/api?page=${page}&results=${results}`);
    return response.data.results.map((user, index) => ({
      id: user.login.username,
      name: `${user.name.first} ${user.name.last}`,
      position: positions[index % positions.length],
      department: departments[index % departments.length],
      email: user.email,
      phone: user.phone,
      status: Math.random() > 0.5 ? 'Full Time' : 'Part Time',
      officeLocation: '2972 Westheimer Rd, Santa Ana, Illinois 85486',
      teamMates: ['Ronald Richards', 'Royal Miles', 'Savannah Nguyen'],
      birthday: '7/27/1998',
      hireYear: '4 Years',
      address: '4140 Parker Rd, Abertown, New Mexico 31134',
      avatar: user.picture.thumbnail
    }));
  } catch (error) {
    console.error('Error fetching team members:', error);
    throw error;
  }
};