import React, { useState, useEffect } from 'react';
import { 
  Container,
  TextField,
  Button,
  Checkbox,
  IconButton,
  Paper,
  Typography,
  Box,
  List,
  ListItem,
  ListItemText,
  ListItemIcon,
  ListItemSecondaryAction
} from '@mui/material';
import { Delete as DeleteIcon } from '@mui/icons-material';
import { pink } from '@mui/material/colors';

type Task = {
  id: number;
  text: string;
  completed: boolean;
};

const TodoApp = () => {
  const [tasks, setTasks] = useState<Task[]>(() => {
    const saved = localStorage.getItem('tasks');
    return saved ? JSON.parse(saved) : [];
  });
  const [newTask, setNewTask] = useState('');

  useEffect(() => {
    localStorage.setItem('tasks', JSON.stringify(tasks));
  }, [tasks]);

  const addTask = () => {
    if (newTask.trim()) {
      setTasks([...tasks, { id: Date.now(), text: newTask, completed: false }]);
      setNewTask('');
    }
  };

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === 'Enter') {
      addTask();
    }
  };

  const toggleTask = (id: number) => {
    setTasks(tasks.map(task => 
      task.id === id ? {
        id: task.id,          
        text: task.text,      
        completed: !task.completed  //spread operator can be used
      } : task
    ));
  };

  const deleteTask = (id: number) => {
    setTasks(tasks.filter(task => task.id !== id));
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Todo App
      </Typography>
      
      <Paper elevation={3} sx={{ p: 3, mb: 2 }}>
        <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
          <TextField
            fullWidth
            variant="outlined"
            placeholder="Add a new task"
            value={newTask}
            onChange={(e) => setNewTask(e.target.value)}
            onKeyPress={handleKeyPress}
            size="small"
          />
          <Button
            variant="contained"
            onClick={addTask}
            disabled={!newTask.trim()}
            sx={{ minWidth: 100 }}
          >
            Add
          </Button>
        </Box>

        <List>
  {tasks.map((task) => (
    <ListItem 
      key={task.id} 
      divider
      sx={{
        textDecoration: task.completed ? 'line-through' : 'none',
        color: task.completed ? 'text.secondary' : 'text.primary',
        pr: 0, 
        position: 'relative' 
      }}
    >
      <ListItemText 
        primary={task.text} 
        sx={{ 
          wordBreak: 'break-word',
          pr: 8 
        }} 
      />
      <Box sx={{
        position: 'absolute',
        right: 8,
        display: 'flex',
        alignItems: 'center'
      }}>
        <Checkbox
          checked={task.completed}
          onChange={() => toggleTask(task.id)}
          sx={{ mr: 1 }}
        />
        <IconButton
          onClick={() => deleteTask(task.id)}
          sx={{ color: pink[500] }}
        >
          <DeleteIcon />
        </IconButton>
      </Box>
    </ListItem>
  ))}
</List>
      </Paper>
    </Container>
  );
};

export default TodoApp;