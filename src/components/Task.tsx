import React from 'react';
import { ListItem, ListItemText, IconButton, Checkbox, Box } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';

interface TaskProps {
  task: { id: number; text: string; completed: boolean };
  onToggle: (id: number) => void;
  onDelete: (id: number) => void;
}

const Task: React.FC<TaskProps> = ({ task, onToggle, onDelete }) => {
  return (
    <ListItem sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', py: 1 }}>
      <Box sx={{ display: 'flex', alignItems: 'center', flex: 1, gap: 2 }}>
        <ListItemText
          primary={task.text}
          sx={{
            overflow: 'hidden',
            textOverflow: 'ellipsis',
            whiteSpace: 'nowrap',
            textDecoration: task.completed ? 'line-through' : 'none', 
          }}
        />
      </Box>
      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
        <Checkbox
          checked={task.completed}
          onChange={() => onToggle(task.id)}
          sx={{
            color: '#ccc',
            '&.Mui-checked': {
              color: '#4caf50',
            },
          }}
        />
        <IconButton onClick={() => onDelete(task.id)} sx={{ color: '#f44336' }}>
          <DeleteIcon />
        </IconButton>
      </Box>
    </ListItem>
  );
};

export default Task;