import React from 'react';
import { Box } from '@mui/material';
import Header from '../components/Header/Header';
import Sidebar from '../components/Sidebar/Sidebar';
import TeamTable from '../components/TeamTable/TeamTable';

const TeamList: React.FC = () => {
  return (
    <Box sx={{ display: 'flex', minHeight: '100vh' }}>
      <Sidebar />
      <Box sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
        <Header />
        <Box component="main" sx={{ flexGrow: 1, backgroundColor: '#F9FAFB' }}>
          <TeamTable />
        </Box>
      </Box>
    </Box>
  );
};

export default TeamList;