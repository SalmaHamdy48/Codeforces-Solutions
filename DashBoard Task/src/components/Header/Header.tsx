// components/Header.tsx
import React, { useState } from 'react';
import { 
  AppBar, 
  Toolbar, 
  Typography, 
  IconButton, 
  Menu, 
  MenuItem, 
  Box,
  Breadcrumbs,
  Link,
  Badge,
  Avatar
} from '@mui/material';
import {
  Flag as FlagIcon,
  Notifications as NotificationsIcon,
  Email as EmailIcon,
  Home as HomeIcon,
  People as PeopleIcon
} from '@mui/icons-material';

interface FlagOption {
  code: string;
  name: string;
}

interface Notification {
  id: number;
  text: string;
  time: string;
}

interface Message {
  id: number;
  text: string;
  sender: string;
  time: string;
}

const Header: React.FC = () => {
  const [flagAnchorEl, setFlagAnchorEl] = useState<null | HTMLElement>(null);
  const [notifAnchorEl, setNotifAnchorEl] = useState<null | HTMLElement>(null);
  const [messageAnchorEl, setMessageAnchorEl] = useState<null | HTMLElement>(null);
  
  const flagOpen = Boolean(flagAnchorEl);
  const notifOpen = Boolean(notifAnchorEl);
  const messageOpen = Boolean(messageAnchorEl);

  const flags: FlagOption[] = [
    { code: 'us', name: 'English (US)' },
    { code: 'fr', name: 'Français' },
    { code: 'es', name: 'Español' },
    { code: 'de', name: 'Deutsch' },
    { code: 'ja', name: '日本語' },
  ];

  const notifications: Notification[] = [
    { id: 1, text: 'New team member joined', time: '2 min ago' },
    { id: 2, text: 'Project deadline updated', time: '1 hour ago' },
    { id: 3, text: 'System maintenance scheduled', time: '3 hours ago' },
  ];

  const messages: Message[] = [
    { id: 1, text: 'About tomorrows meeting', sender: 'John Doe', time: '10:30 AM' },
    { id: 2, text: 'Project documents', sender: 'Jane Smith', time: 'Yesterday' },
  ];

  const handleFlagClick = (event: React.MouseEvent<HTMLElement>) => {
    setFlagAnchorEl(event.currentTarget);
  };

  const handleNotifClick = (event: React.MouseEvent<HTMLElement>) => {
    setNotifAnchorEl(event.currentTarget);
  };

  const handleMessageClick = (event: React.MouseEvent<HTMLElement>) => {
    setMessageAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setFlagAnchorEl(null);
    setNotifAnchorEl(null);
    setMessageAnchorEl(null);
  };

  return (
    <Box>
      <AppBar 
        position="static" 
        color="default" 
        elevation={0} 
        sx={{ 
          backgroundColor: 'white',
          borderBottom: '1px solid #E5E7EB'
        }}
      >
        <Toolbar sx={{ justifyContent: 'space-between', paddingX: 3 }}>
          {/* Breadcrumb navigation */}
          <Breadcrumbs aria-label="breadcrumb">
            <Link
              underline="hover"
              color="inherit"
              href="#"
              sx={{ display: 'flex', alignItems: 'center' }}
            >
              <HomeIcon sx={{ mr: 0.5, fontSize: 20 }} />
              <Typography variant="body1" component="span">
                Admin Dashboard
              </Typography>
            </Link>
            <Typography
              color="text.primary"
              sx={{ display: 'flex', alignItems: 'center' }}
            >
              <PeopleIcon sx={{ mr: 0.5, fontSize: 20 }} />
              <Typography variant="body1" component="span">
                Team List
              </Typography>
            </Typography>
          </Breadcrumbs>

          {/* Icons section */}
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
            {/* Flag dropdown */}
            <IconButton
              aria-label="language"
              onClick={handleFlagClick}
              size="small"
              sx={{ color: 'text.secondary' }}
            >
              <FlagIcon />
            </IconButton>
            <Menu
              anchorEl={flagAnchorEl}
              open={flagOpen}
              onClose={handleClose}
              PaperProps={{
                sx: {
                  mt: 1.5,
                  minWidth: 180,
                  boxShadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1)',
                  '& .MuiMenuItem-root': {
                    fontSize: 14,
                    padding: '8px 16px'
                  }
                }
              }}
            >
              {flags.map((flag) => (
                <MenuItem key={flag.code} onClick={handleClose}>
                  <span style={{ fontWeight: 500, marginRight: 8 }}>{flag.code.toUpperCase()}</span>
                  <span>{flag.name}</span>
                </MenuItem>
              ))}
            </Menu>

            {/* Notifications */}
            <IconButton
              aria-label="notifications"
              onClick={handleNotifClick}
              size="small"
              sx={{ color: 'text.secondary' }}
            >
              <Badge badgeContent={3} color="error">
                <NotificationsIcon />
              </Badge>
            </IconButton>
            <Menu
              anchorEl={notifAnchorEl}
              open={notifOpen}
              onClose={handleClose}
              PaperProps={{
                sx: {
                  mt: 1.5,
                  width: 320,
                  boxShadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1)',
                  '& .MuiMenuItem-root': {
                    padding: '8px 16px'
                  }
                }
              }}
            >
              <Typography variant="subtitle1" sx={{ padding: '8px 16px', fontWeight: 600 }}>
                Notifications
              </Typography>
              {notifications.map((notif) => (
                <MenuItem key={notif.id} onClick={handleClose}>
                  <Box>
                    <Typography variant="body2">{notif.text}</Typography>
                    <Typography variant="caption" sx={{ color: 'text.secondary', fontSize: 12 }}>
                      {notif.time}
                    </Typography>
                  </Box>
                </MenuItem>
              ))}
            </Menu>

            {/* Messages */}
            <IconButton
              aria-label="messages"
              onClick={handleMessageClick}
              size="small"
              sx={{ color: 'text.secondary' }}
            >
              <Badge badgeContent={2} color="error">
                <EmailIcon />
              </Badge>
            </IconButton>
            <Menu
              anchorEl={messageAnchorEl}
              open={messageOpen}
              onClose={handleClose}
              PaperProps={{
                sx: {
                  mt: 1.5,
                  width: 320,
                  boxShadow: '0 1px 3px 0 rgba(0, 0, 0, 0.1)',
                  '& .MuiMenuItem-root': {
                    padding: '12px 16px'
                  }
                }
              }}
            >
              <Typography variant="subtitle1" sx={{ padding: '8px 16px', fontWeight: 600 }}>
                Messages
              </Typography>
              {messages.map((msg) => (
                <MenuItem key={msg.id} onClick={handleClose}>
                  <Box sx={{ display: 'flex', alignItems: 'center' }}>
                    <Avatar sx={{ width: 32, height: 32, mr: 2 }}>
                      {msg.sender.charAt(0)}
                    </Avatar>
                    <Box>
                      <Typography variant="body2" sx={{ fontWeight: 500 }}>
                        {msg.sender}
                      </Typography>
                      <Typography variant="body2" sx={{ fontSize: 13 }}>
                        {msg.text}
                      </Typography>
                      <Typography variant="caption" sx={{ color: 'text.secondary', fontSize: 12 }}>
                        {msg.time}
                      </Typography>
                    </Box>
                  </Box>
                </MenuItem>
              ))}
            </Menu>

            {/* User avatar */}
            <Avatar 
              sx={{ width: 36, height: 36, ml: 1 }}
              src="/path/to/user-avatar.jpg"
            >
              A
            </Avatar>
          </Box>
        </Toolbar>
      </AppBar>
    </Box>
  );
};

export default Header;