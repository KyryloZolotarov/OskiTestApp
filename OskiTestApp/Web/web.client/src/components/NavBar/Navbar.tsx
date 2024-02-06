import {useAuth} from "../../Auth/AuthProvider";
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import {useNavigate} from "react-router-dom";
import LogoutIcon from '@mui/icons-material/Logout';
import IconButton from '@mui/material/IconButton';

const Navbar = () => {
    const {logout} = useAuth();
    const navigate = useNavigate();

    const handleAvailableClick = () => {
        navigate(`/`);
    };

    const handlePassedClick = () => {
        navigate(`/passedtests`);
    };
    
    const handleLogout = () => {
        logout();
        // Дополнительные действия при выходе
    };
    

    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Toolbar variant="dense">
                    <Typography variant="h5" color="inherit" component="div" sx={{ mr: 2 }}>
                        OskiTest
                    </Typography>
                    <Button onClick={() => handleAvailableClick()} variant="text" color="inherit" sx={{ mr: 2 }}>Available Tests</Button>
                    <Button onClick={() => handlePassedClick()} variant="text" color="inherit" sx={{ mr: 2 }}>Passed Tests</Button>
                    <Box sx={{ flexGrow: 1 }} />
                    <IconButton onClick={() => handleLogout()} aria-label="fingerprint" color="secondary">
                        <LogoutIcon />
                    </IconButton>
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default Navbar;
