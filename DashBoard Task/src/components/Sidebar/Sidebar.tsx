import React, { useState } from 'react';
import { Box, Drawer, List, ListItem, ListItemIcon, Divider, IconButton } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import DashboardIcon from '@mui/icons-material/Dashboard';
import PeopleIcon from '@mui/icons-material/People';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import FolderIcon from '@mui/icons-material/Folder';
import PersonIcon from '@mui/icons-material/Person';

const Sidebar: React.FC = () => {
  const [open, setOpen] = useState(false);
  const drawerWidth = 60;

  const handleDrawerToggle = () => {
    setOpen(!open);
  };

  return (
    <>
      <IconButton
        color="inherit"
        aria-label="open drawer"
        edge="start"
        onClick={handleDrawerToggle}
        sx={{
          display: { xs: 'block', sm: 'none' }, // Show only on mobile
          position: 'fixed',
          top: 10,
          left: 10,
          zIndex: 1200,
        }}
      >
        <MenuIcon />
      </IconButton>
      <Drawer
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          '& .MuiDrawer-paper': {
            width: drawerWidth,
            boxSizing: 'border-box',
            backgroundColor: '#f8fafc',
            borderRight: '1px solid #e5e7eb',
            display: 'flex',
            flexDirection: 'column',
            transition: (theme) => theme.transitions.create('width', {
              easing: theme.transitions.easing.sharp,
              duration: theme.transitions.duration.leavingScreen,
            }),
            ...(open && {
              width: drawerWidth,
            }),
            ...(open === false && {
              width: { xs: 0, sm: drawerWidth },
              '& .MuiDrawer-paper': { width: { xs: 0, sm: drawerWidth } },
            }),
          },
        }}
        variant="permanent"
        open={open}
        onClose={handleDrawerToggle}
      >
        <Box sx={{ p: 3, display: 'flex', justifyContent: 'center' }}>
          <DashboardIcon sx={{ fontSize: 30, color: '#1976d2' }} />
        </Box>
        <Divider />
        <List sx={{ flexGrow: 1, pt: 2 }}>
          {[
            { icon: <PeopleIcon />, active: true },
            { icon: <DashboardIcon /> },
            { icon: <CalendarTodayIcon /> },
            { icon: <FolderIcon /> },
          ].map((item, index) => (
            <ListItem
              key={index}
              disablePadding
              sx={{
                justifyContent: 'center',
                backgroundColor: item.active ? 'rgba(25, 118, 210, 0.08)' : 'inherit',
                borderLeft: item.active ? '3px solid #1976d2' : 'none',
                mb: 2,
              }}
            >
              <ListItemIcon sx={{ justifyContent: 'center', color: item.active ? '#1976d2' : '#6b7280' }}>
                {item.icon}
              </ListItemIcon>
            </ListItem>
          ))}
        </List>
        <Box sx={{ p: 3, display: 'flex', justifyContent: 'center' }}>
          <PersonIcon sx={{ fontSize: 24, color: '#6b7280' }} />
        </Box>
      </Drawer>
    </>
  );
};

export default Sidebar;