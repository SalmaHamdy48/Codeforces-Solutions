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
import './Header.css';

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
    <Box className="header-container">
      <AppBar position="static" color="default" elevation={0} className="app-bar">
        <Toolbar className="toolbar">
          {/* Breadcrumb navigation */}
          <Breadcrumbs aria-label="breadcrumb" className="breadcrumbs">
            <Link
              underline="hover"
              color="inherit"
              href="#"
              className="breadcrumb-link"
            >
              <HomeIcon className="breadcrumb-icon" />
              <Typography variant="body1" component="span">
                Admin Dashboard
              </Typography>
            </Link>
            <Typography
              color="text.primary"
              className="breadcrumb-current"
            >
              <PeopleIcon className="breadcrumb-icon" />
              <Typography variant="body1" component="span">
                Team List
              </Typography>
            </Typography>
          </Breadcrumbs>

          {/* Icons section */}
          <Box className="icons-container">
            {/* Flag dropdown */}
            <IconButton
              aria-label="language"
              onClick={handleFlagClick}
              className="icon-button"
            >
              <FlagIcon />
            </IconButton>
            <Menu
              anchorEl={flagAnchorEl}
              open={flagOpen}
              onClose={handleClose}
              className="dropdown-menu"
            >
              {flags.map((flag) => (
                <MenuItem 
                  key={flag.code} 
                  onClick={handleClose}
                  className="menu-item"
                >
                  <span className="flag-code">{flag.code.toUpperCase()}</span>
                  <span className="flag-name">{flag.name}</span>
                </MenuItem>
              ))}
            </Menu>

            {/* Notifications */}
            <IconButton
              aria-label="notifications"
              onClick={handleNotifClick}
              className="icon-button"
            >
              <Badge badgeContent={3} color="error">
                <NotificationsIcon />
              </Badge>
            </IconButton>
            <Menu
              anchorEl={notifAnchorEl}
              open={notifOpen}
              onClose={handleClose}
              className="dropdown-menu notification-menu"
            >
              <Typography variant="subtitle1" className="menu-title">
                Notifications
              </Typography>
              {notifications.map((notif) => (
                <MenuItem key={notif.id} onClick={handleClose} className="menu-item">
                  <div className="notification-content">
                    <Typography variant="body2">{notif.text}</Typography>
                    <Typography variant="caption" className="time-text">
                      {notif.time}
                    </Typography>
                  </div>
                </MenuItem>
              ))}
            </Menu>

            {/* Messages */}
            <IconButton
              aria-label="messages"
              onClick={handleMessageClick}
              className="icon-button"
            >
              <Badge badgeContent={2} color="error">
                <EmailIcon />
              </Badge>
            </IconButton>
            <Menu
              anchorEl={messageAnchorEl}
              open={messageOpen}
              onClose={handleClose}
              className="dropdown-menu message-menu"
            >
              <Typography variant="subtitle1" className="menu-title">
                Messages
              </Typography>
              {messages.map((msg) => (
                <MenuItem key={msg.id} onClick={handleClose} className="menu-item">
                  <div className="message-content">
                    <Avatar sx={{ width: 32, height: 32, mr: 2 }}>
                      {msg.sender.charAt(0)}
                    </Avatar>
                    <div>
                      <Typography variant="body2" className="sender-name">
                        {msg.sender}
                      </Typography>
                      <Typography variant="body2" className="message-text">
                        {msg.text}
                      </Typography>
                      <Typography variant="caption" className="time-text">
                        {msg.time}
                      </Typography>
                    </div>
                  </div>
                </MenuItem>
              ))}
            </Menu>

            {/* User avatar */}
            <Avatar 
              sx={{ width: 36, height: 36, ml: 2 }}
              src="/path/to/user-avatar.jpg"
              className="user-avatar"
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