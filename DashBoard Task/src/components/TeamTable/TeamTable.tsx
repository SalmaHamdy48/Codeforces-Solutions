/* eslint-disable @typescript-eslint/no-unused-vars */
import React, { useState } from 'react';
import {
  Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
  Paper, Checkbox, IconButton, Typography, TextField, Button, Box,
  Avatar, Skeleton, Accordion, AccordionSummary, AccordionDetails,
  InputAdornment,
  Stack,
} from '@mui/material';
import { ExpandMore, Search as SearchIcon } from '@mui/icons-material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import AddIcon from '@mui/icons-material/Add';
import Sidebar from '../Sidebar/Sidebar';
import { useQuery } from '@tanstack/react-query';
import { fetchTeamMembers, TeamMember } from '../../services/api';

const TeamTable: React.FC = () => {
  const [selected, setSelected] = useState<string[]>([]);
  const [page, setPage] = useState(1);
  const rowsPerPage = 5;
  const totalItems = 48;

  const { data: members, isLoading } = useQuery<TeamMember[]>({
    queryKey: ['teamMembers', page],
    queryFn: () => fetchTeamMembers(page, rowsPerPage),
    staleTime: 5000
  });

  const handleSelectAll = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.checked && members) {
      const newSelected = members.map((member) => member.id);
      setSelected(newSelected);
    } else {
      setSelected([]);
    }
  };

  const handleSelect = (id: string) => {
    setSelected(prev => prev.includes(id) 
      ? prev.filter(item => item !== id) 
      : [...prev, id]);
  };

  const handleEdit = (id: string) => {
    const memberToEdit = members?.find(member => member.id === id);
    if (memberToEdit) {
      console.log('Editing member:', memberToEdit);
    }
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Are you sure you want to delete this team member?')) {
      setSelected(prev => prev.filter(memberId => memberId !== id));
    }
  };
/*
  const handlePageChange = (_event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };
*/
  const totalPages = Math.ceil(totalItems / rowsPerPage);

  return (
    <Box sx={{ display: 'flex' }}>
      <Sidebar />
      <Box sx={{ flexGrow: 1, p: 3, ml: { sm: 0 } }}>
        {/* Header Section */}
        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
          <Typography variant="h5" sx={{ fontWeight: 600 }}>Team List</Typography>
          <Typography variant="body2" color="text.secondary">
            Admin Dashboard &gt; Team List
          </Typography>
        </Box>

        {/* Search and Add User Section */}
        <Box sx={{ 
          display: 'flex', 
          flexDirection: { xs: 'column', sm: 'row' }, 
          justifyContent: 'space-between', 
          alignItems: 'center', 
          mb: 3,
          gap: 2
        }}>
          <Box sx={{ 
            display: 'flex', 
            alignItems: 'center', 
            gap: 1,
            width: { xs: '100%', sm: 'auto' }
          }}>
            {selected.length > 0 && (
              <Typography variant="body2" sx={{ whiteSpace: 'nowrap' }}>
                {selected.length} Selected
              </Typography>
            )}
            <TextField
              placeholder="Search Task"
              size="small"
              sx={{ 
                width: { xs: '100%', sm: 300 },
                '& .MuiOutlinedInput-root': {
                  backgroundColor: '#F9FAFB'
                }
              }}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <SearchIcon />
                  </InputAdornment>
                ),
              }}
            />
          </Box>
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            sx={{ 
              width: { xs: '100%', sm: 'auto' },
              whiteSpace: 'nowrap'
            }}
          >
            Add User
          </Button>
        </Box>

        {/* Table Section */}
        <TableContainer 
          component={Paper} 
          sx={{ 
            boxShadow: 'none',
            border: '1px solid #E5E7EB',
            borderRadius: '8px',
            mb: 2,
            width: 'fit-content',
            maxWidth: '100%',
          }}
        >
          <Table sx={{ 
            minWidth: 1300,
            '& .MuiTableCell-root': {
              padding: '12px 16px',
              fontSize: '0.875rem'
            }
          }}>
            <TableHead sx={{ backgroundColor: '#F9FAFB' }}>
              <TableRow>
                <TableCell padding="checkbox" sx={{ width: 48 }}>
                  <Checkbox
                    indeterminate={selected.length > 0 && members && selected.length < members.length}
                    checked={members && members.length > 0 && selected.length === members.length}
                    onChange={handleSelectAll}
                    size="small"
                  />
                </TableCell>
                <TableCell sx={{ width: 150 }}>Name</TableCell>
                <TableCell sx={{ width: 120 }}>Position</TableCell>
                <TableCell sx={{ width: 120 }}>Department</TableCell>
                <TableCell sx={{ width: 180 }}>Email</TableCell>
                <TableCell sx={{ width: 120 }}>Phone</TableCell>
                <TableCell sx={{ width: 100 }}>Status</TableCell>
                <TableCell sx={{ width: 100 }}>Edit</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {isLoading ? (
                <TableSkeleton rowsPerPage={rowsPerPage} />
              ) : (
                members?.map((member) => (
                  <React.Fragment key={member.id}>
                    <TableRow hover selected={selected.includes(member.id)}>
                      <TableCell padding="checkbox">
                        <Checkbox
                          checked={selected.includes(member.id)}
                          onChange={() => handleSelect(member.id)}
                          size="small"
                        />
                      </TableCell>
                      <TableCell>
                        <Box sx={{ display: 'flex', alignItems: 'center', gap: 1.5 }}>
                          <Avatar src={member.avatar} alt={member.name} sx={{ width: 36, height: 36 }} />
                          <Typography>{member.name}</Typography>
                        </Box>
                      </TableCell>
                      <TableCell>{member.position}</TableCell>
                      <TableCell>{member.department}</TableCell>
                      <TableCell>{member.email}</TableCell>
                      <TableCell>{member.phone}</TableCell>
                      <TableCell>
                        <Box sx={{
                          display: 'inline-block',
                          px: 1.5,
                          py: 0.5,
                          borderRadius: '12px',
                          backgroundColor: member.status === 'Full Time' ? '#E0F7FA' : '#FFF8E1',
                          color: member.status === 'Full Time' ? '#006064' : '#FF8F00',
                          fontSize: '0.75rem',
                          fontWeight: 500
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
                              backgroundColor: '#EDF2F7',
                              '&:hover': { backgroundColor: '#E2E8F0' }
                            }}
                          >
                            <EditIcon fontSize="small" sx={{ color: '#4A5568' }} />
                          </IconButton>
                          <IconButton 
                            size="small"
                            onClick={() => handleDelete(member.id)}
                            sx={{ 
                              backgroundColor: '#FEE2E2',
                              '&:hover': { backgroundColor: '#FECACA' }
                            }}
                          >
                            <DeleteIcon fontSize="small" sx={{ color: '#DC2626' }} />
                          </IconButton>
                        </Box>
                      </TableCell>
                    </TableRow>
                    
                    {/* Accordion for Details */}
                    <TableRow>
                      <TableCell colSpan={8} sx={{ p: 0, borderBottom: '1px solid #E5E7EB' }}>
                        <Accordion sx={{ 
                          boxShadow: 'none',
                          '&:before': { display: 'none' },
                          '&.Mui-expanded': { m: 0 }
                        }}>
                          <AccordionSummary 
                            expandIcon={<ExpandMore />}
                            sx={{ 
                              minHeight: '30px !important',
                              '& .MuiAccordionSummary-content': { m: 0 }
                            }}
                          >
                            <Typography variant="body2" fontWeight={500}>Details</Typography>
                          </AccordionSummary>
                          <AccordionDetails sx={{ pt: 0, pb: 2 }}>
                            <Box sx={{ 
                              display: 'flex',
                              flexWrap: 'wrap',
                              gap: 3,
                              '& > *': { minWidth: '150px' }
                            }}>
                              <DetailItem label="Office Location" value={member.officeLocation} />
                              <DetailItem label="Team Mates" value={member.teamMates?.join(', ')} />
                              <DetailItem label="Birthday" value={member.birthday} />
                              <DetailItem label="HR Year" value={member.hireYear} />
                              <DetailItem label="Address" value={member.address} />
                            </Box>
                          </AccordionDetails>
                        </Accordion>
                      </TableCell>
                    </TableRow>
                  </React.Fragment>
                ))
              )}
            </TableBody>
          </Table>
        </TableContainer>

        {/* Pagination */}
<Box sx={{ 
  display: 'flex', 
  justifyContent: 'flex-end', 
  alignItems: 'center', 
  width: '100%',
  mt: 2
}}>
  <Typography variant="body2" color="text.secondary">
    {`${(page - 1) * rowsPerPage + 1} - ${Math.min(page * rowsPerPage, totalItems)} of ${totalItems}`}
  </Typography>
  
  <Stack direction="row" spacing={1}>
    <Button 
      variant="outlined" 
      size="small"
      disabled={page === 1}
      onClick={() => setPage(p => Math.max(1, p - 1))}
      sx={{ minWidth: 32 }}
    >
      &lt;
    </Button>
    <Button 
      variant="outlined" 
      size="small"
      disabled={page === totalPages}
      onClick={() => setPage(p => Math.min(totalPages, p + 1))}
      sx={{ minWidth: 32 }}
    >
      &gt;
    </Button>
  </Stack>
</Box>
      </Box>
    </Box>
  );
};

// Helper components
const TableSkeleton: React.FC<{ rowsPerPage: number }> = ({ rowsPerPage }) => (
  <>
    {Array(rowsPerPage).fill(0).map((_, i) => (
      <React.Fragment key={i}>
        {/* Main row skeleton - matches exact height of loaded row */}
        <TableRow sx={{ height: 72 }}>
          <TableCell padding="checkbox">
            <Skeleton variant="rectangular" width={18} height={18} />
          </TableCell>
          <TableCell>
            <Box sx={{ display: 'flex', alignItems: 'center', gap: 1.5 }}>
              <Skeleton variant="circular" width={36} height={36} />
              <Skeleton variant="text" width={120} height={24} />
            </Box>
          </TableCell>
          <TableCell><Skeleton variant="text" width={100} height={24} /></TableCell>
          <TableCell><Skeleton variant="text" width={100} height={24} /></TableCell>
          <TableCell><Skeleton variant="text" width={150} height={24} /></TableCell>
          <TableCell><Skeleton variant="text" width={100} height={24} /></TableCell>
          <TableCell>
            <Skeleton 
              variant="rectangular" 
              width={80} 
              height={24} 
              sx={{ borderRadius: '12px' }} 
            />
          </TableCell>
          <TableCell>
            <Box sx={{ display: 'flex', gap: 1 }}>
              <Skeleton 
                variant="rectangular" 
                width={32} 
                height={32} 
                sx={{ borderRadius: '4px' }} 
              />
              <Skeleton 
                variant="rectangular" 
                width={32} 
                height={32} 
                sx={{ borderRadius: '4px' }} 
              />
            </Box>
          </TableCell>
        </TableRow>
        
        {/* Accordion skeleton - matches expanded details height */}
        <TableRow>
          <TableCell colSpan={8} sx={{ p: 0, borderBottom: '1px solid #E5E7EB' }}>
            <Box sx={{ p: 2, height: 120 }}>
              <Skeleton variant="text" width={60} height={24} sx={{ mb: 1 }} />
              <Box sx={{ 
                display: 'flex',
                flexWrap: 'wrap',
                gap: 3,
                '& > *': { 
                  minWidth: '150px',
                  flex: '1 1 150px'
                }
              }}>
                {Array(5).fill(0).map((_, j) => (
                  <Box key={j}>
                    <Skeleton variant="text" width={100} height={20} />
                    <Skeleton variant="text" width={140} height={24} sx={{ mt: 0.5 }} />
                  </Box>
                ))}
              </Box>
            </Box>
          </TableCell>
        </TableRow>
      </React.Fragment>
    ))}
  </>
);

const DetailItem: React.FC<{ label: string; value?: string }> = ({ label, value }) => (
  <Box>
    <Typography variant="caption" color="text.secondary" display="block">
      {label}
    </Typography>
    <Typography variant="body2">{value || '-'}</Typography>
  </Box>
);

export default TeamTable;