import React, { useState } from 'react';
import { TextField, Button, Box } from '@mui/material';

interface TaskFormProps {
  onAdd: (text: string) => void;
}

const TaskForm: React.FC<TaskFormProps> = ({ onAdd }) => {
  const [taskText, setTaskText] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (taskText.trim()) {
      onAdd(taskText);
      setTaskText('');
    }
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ display: 'flex', gap: 2, mb: 2 }}>
      <TextField
        placeholder="Add a new task"
        value={taskText}
        onChange={(e) => setTaskText(e.target.value)}
        fullWidth
        variant="outlined"
        size="small"
      />
      <Button
        type="submit"
        variant="contained"
        sx={{
          bgcolor: taskText.trim() ? '#1976d2' : '#ccc',
          color: '#fff',
          '&:hover': {
            bgcolor: taskText.trim() ? '#1565c0' : '#bbb',
          },
        }}
      >
        ADD
      </Button>
    </Box>
  );
};

export default TaskForm;