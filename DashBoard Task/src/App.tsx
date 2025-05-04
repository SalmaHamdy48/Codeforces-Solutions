
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import TeamList from './pages/TeamList';

const theme = createTheme({
  palette: {
    primary: {
      main: '#4F46E5',
    },
    secondary: {
      main: '#EC4899',
    },
    background: {
      default: '#F9FAFB',
      paper: '#FFFFFF',
    },
    text: {
      primary: '#111827',
      secondary: '#6B7280',
    },
    divider: '#E5E7EB',
    action: {
      selected: 'rgba(25, 118, 210, 0.08)',
      hover: '#F3F4F6'
    }
  },
  components: {
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          fontSize: '14px',
          fontWeight: 500,
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          borderBottomColor: '#E5E7EB'
        }
      }
    },
    MuiDrawer: {
      styleOverrides: {
        paper: {
          borderRight: 'none',
          boxShadow: '0 1px 2px 0 rgba(0, 0, 0, 0.05)'
        }
      }
    }
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<TeamList />} />
          <Route path="/team" element={<TeamList />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
}

export default App;