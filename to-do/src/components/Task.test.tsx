import { render, screen } from '@testing-library/react';
import Task from './Task';

test('renders task with text, checkbox, and delete button', () => {
  const task = { id: 1, text: 'Test Task', completed: false };
  render(<Task task={task} onToggle={() => {}} onDelete={() => {}} />);
  expect(screen.getByText('Test Task')).toBeInTheDocument();
  expect(screen.getByRole('checkbox')).not.toBeChecked();
  expect(screen.getByRole('button')).toBeInTheDocument();
});