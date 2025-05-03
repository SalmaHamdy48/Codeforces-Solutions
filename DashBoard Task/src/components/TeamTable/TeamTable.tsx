import React, { useState, useEffect } from 'react';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, Checkbox, IconButton, Typography, TextField, Button, Box,
  Avatar, Skeleton
} from '@mui/material';
import { ChevronLeft, ChevronRight } from '@mui/icons-material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import { fetchTeamMembers, TeamMember } from '../../services/api';
import './TeamTable.css';

const TeamTable: React.FC = () => {
  const [members, setMembers] = useState<TeamMember[]>([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState<string[]>([]);
  const [page, setPage] = useState(1);
  const rowsPerPage = 10;
  const totalItems = 48;

  useEffect(() => {
    const loadData = async () => {
      setLoading(true);
      try {
        const data = await fetchTeamMembers(page, rowsPerPage);
        setMembers(data);
      } catch (error) {
        console.error('Failed to load data:', error);
      } finally {
        setLoading(false);
      }
    };
    loadData();
  }, [page]);

  const handleSelectAll = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSelected(e.target.checked ? members.map(m => m.id) : []);
  };

  const handleSelect = (id: string) => {
    setSelected(prev => prev.includes(id) 
      ? prev.filter(item => item !== id) 
      : [...prev, id]);
  };

  const handleEdit = (id: string) => {
    const memberToEdit = members.find(member => member.id === id);
    if (memberToEdit) {
      console.log('Editing member:', memberToEdit);
      // Add your edit logic here
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Are you sure you want to delete this team member?')) {
      setMembers(prev => prev.filter(member => member.id !== id));
      setSelected(prev => prev.filter(memberId => memberId !== id));
    }
  };

  return (
    <Box sx={{ p: 3, mt: 8 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
        <Typography variant="h5" sx={{ fontWeight: 600 }}>Team List</Typography>
        <Typography variant="body1" color="text.secondary">Admin Dashboard → Team List</Typography>
      </Box>

      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <TextField
          label="Search Task"
          variant="outlined"
          size="small"
          sx={{ width: 300 }}
        />
        <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
          {selected.length > 0 && (
            <Typography variant="body2" color="text.secondary">
              {selected.length} Selected
            </Typography>
          )}
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            sx={{
              backgroundColor: '#4f46e5',
              '&:hover': {
                backgroundColor: '#4338ca',
              }
            }}
          >
            Add User
          </Button>
        </Box>
      </Box>

      <TableContainer component={Paper} sx={{ 
        boxShadow: '0 1px 2px rgba(0, 0, 0, 0.05)',
        border: '1px solid #e5e7eb',
        borderRadius: '8px'
      }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox">
                <Checkbox
                  indeterminate={selected.length > 0 && selected.length < members.length}
                  checked={members.length > 0 && selected.length === members.length}
                  onChange={handleSelectAll}
                />
              </TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Position</TableCell>
              <TableCell>Department</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Phone</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Edit</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loading ? (
              <TableSkeleton rowsPerPage={rowsPerPage} />
            ) : (
              members.map((member) => (
                <React.Fragment key={member.id}>
                  <TableRow hover selected={selected.includes(member.id)}>
                    <TableCell padding="checkbox">
                      <Checkbox
                        checked={selected.includes(member.id)}
                        onChange={() => handleSelect(member.id)}
                      />
                    </TableCell>
                    <TableCell>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
                        <Avatar src={member.avatar} alt={member.name} sx={{ width: 32, height: 32 }} />
                        {member.name}
                      </Box>
                    </TableCell>
                    <TableCell>{member.position}</TableCell>
                    <TableCell>{member.department}</TableCell>
                    <TableCell>{member.email}</TableCell>
                    <TableCell>{member.phone}</TableCell>
                    <TableCell>
                      <Box sx={{
                        display: 'inline-flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        padding: '2px 8px',
                        borderRadius: '10px',
                        fontSize: '12px',
                        fontWeight: 500,
                        backgroundColor: member.status === 'Full Time' ? '#EFF6FF' : '#FEF3C7',
                        color: member.status === 'Full Time' ? '#1E40AF' : '#92400E',
                      }}>
                        {member.status}
                      </Box>
                    </TableCell>
                    <TableCell>
                      <Box sx={{ display: 'flex', gap: 1 }}>
                        <IconButton 
                          size="small"
                          onClick={() => handleEdit(member.id)}
                          sx={{
                            backgroundColor: '#e5e7eb',
                            borderRadius: '4px',
                            '&:hover': {
                              backgroundColor: '#d1d5db'
                            }
                          }}
                        >
                          <EditIcon fontSize="small" sx={{ color: '#4b5563' }} />
                        </IconButton>
                        <IconButton 
                          size="small"
                          onClick={() => handleDelete(member.id)}
                          sx={{
                            backgroundColor: '#fee2e2',
                            borderRadius: '4px',
                            '&:hover': {
                              backgroundColor: '#fecaca'
                            }
                          }}
                        >
                          <DeleteIcon fontSize="small" sx={{ color: '#dc2626' }} />
                        </IconButton>
                      </Box>
                    </TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell colSpan={8} sx={{ padding: '16px !important', backgroundColor: '#f9fafb' }}>
                      <Box sx={{ display: 'flex', gap: 4, padding: '8px 0' }}>
                        <DetailColumn title="Office Location" value={member.officeLocation} />
                        <DetailColumn title="Team Mates" values={member.teamMates} />
                        <DetailColumn title="Birthday" value={member.birthday} />
                        <DetailColumn title="Hire Year" value={member.hireYear} />
                        <DetailColumn title="Address" value={member.address} />
                      </Box>
                    </TableCell>
                  </TableRow>
                </React.Fragment>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <Box sx={{ 
        display: 'flex', 
        justifyContent: 'flex-end', 
        alignItems: 'center', 
        gap: 2, 
        mt: 3,
        p: 2,
        backgroundColor: '#ffffff',
        borderRadius: '8px',
        border: '1px solid #e5e7eb',
        boxShadow: '0 1px 2px rgba(0, 0, 0, 0.05)'
      }}>
        <Typography variant="body2" color="text.secondary">
          {`${(page - 1) * rowsPerPage + 1} - ${Math.min(page * rowsPerPage, totalItems)} of ${totalItems}`}
        </Typography>
        <Box sx={{ display: 'flex', gap: '4px' }}>
          <IconButton 
            onClick={() => setPage(p => Math.max(p - 1, 1))} 
            disabled={page === 1}
            size="small"
            sx={{
              border: '1px solid #e5e7eb',
              borderRadius: '4px',
              '&:disabled': {
                opacity: 0.5
              }
            }}
          >
            <ChevronLeft fontSize="small" />
          </IconButton>
          <IconButton
            onClick={() => setPage(p => p + 1)}
            disabled={page * rowsPerPage >= totalItems}
            size="small"
            sx={{
              border: '1px solid #e5e7eb',
              borderRadius: '4px',
              '&:disabled': {
                opacity: 0.5
              }
            }}
          >
            <ChevronRight fontSize="small" />
          </IconButton>
        </Box>
      </Box>
    </Box>
  );
};

const TableSkeleton: React.FC<{ rowsPerPage: number }> = ({ rowsPerPage }) => {
  return (
    <>
      {Array.from({ length: rowsPerPage }).map((_, index) => (
        <React.Fragment key={index}>
          <TableRow>
            <TableCell padding="checkbox">
              <Skeleton variant="rectangular" width={20} height={20} />
            </TableCell>
            <TableCell>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 2 }}>
                <Skeleton variant="circular" width={32} height={32} />
                <Skeleton variant="text" width={120} />
              </Box>
            </TableCell>
            <TableCell><Skeleton variant="text" /></TableCell>
            <TableCell><Skeleton variant="text" /></TableCell>
            <TableCell><Skeleton variant="text" /></TableCell>
            <TableCell><Skeleton variant="text" /></TableCell>
            <TableCell>
              <Skeleton variant="text" width={80} height={24} />
            </TableCell>
            <TableCell>
              <Box sx={{ display: 'flex', gap: 1 }}>
                <Skeleton variant="circular" width={24} height={24} />
                <Skeleton variant="circular" width={24} height={24} />
              </Box>
            </TableCell>
          </TableRow>
          <TableRow>
            <TableCell colSpan={8} sx={{ padding: '16px !important' }}>
              <Box sx={{ display: 'flex', gap: 4, padding: '8px 0' }}>
                {Array.from({ length: 5 }).map((_, i) => (
                  <Box key={i} sx={{ minWidth: 150 }}>
                    <Skeleton variant="text" width={100} height={20} />
                    <Skeleton variant="text" width={150} height={20} />
                  </Box>
                ))}
              </Box>
            </TableCell>
          </TableRow>
        </React.Fragment>
      ))}
    </>
  );
};

const DetailColumn: React.FC<{ title: string; value?: string; values?: string[] }> = ({ title, value, values }) => (
  <Box sx={{ minWidth: 150 }}>
    <Typography variant="subtitle2" sx={{ fontSize: '12px', fontWeight: 500, color: '#6b7280', mb: 0.5 }}>
      {title}
    </Typography>
    {values ? (
      values.map(v => (
        <Typography key={v} sx={{ fontSize: '14px' }}>{v}</Typography>
      ))
    ) : (
      <Typography sx={{ fontSize: '14px' }}>{value}</Typography>
    )}
  </Box>
);

export default TeamTable;