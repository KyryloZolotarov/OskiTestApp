import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import CheckCircleIcon from '@mui/icons-material/CheckCircle';
import CancelIcon from '@mui/icons-material/Cancel';
import QuizIcon from '@mui/icons-material/Quiz';
import Typography from '@mui/material/Typography';

interface PassedTestViewModel {
    id: number;
    name: string;
    mark: number;
}

const PassedTests = () => {
    const [testsData, setTestsData] = useState<PassedTestViewModel[] | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Fetching data from the server
        fetch("http://localhost:5003/test/getPassedTests", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
            credentials: "include",
        })
            .then((response) => response.json())
            .then((data) => setTestsData(data))
            .catch((error) => console.error("Error loading data", error));
    }, []);

    return (
        <div style={{ width: '100%' }}>
            <Typography variant="h5" sx={{ pl: 2, pt: 2 }}>Passed Tests</Typography>
            {testsData ? (
                <List sx={{ width: '100%', borderTop: '1px solid #e0e0e0', borderBottom: '1px solid #e0e0e0' }}> {/* Add border-top and border-bottom to List */}
                    {testsData.map((test, index) => (
                        <ListItem
                            key={test.id}
                            sx={{
                                width: '100%',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center', // Align items vertically in the center
                                borderBottom: index !== testsData.length - 1 ? '1px solid #e0e0e0' : 'none',
                            }}
                        >
                            <ListItemAvatar sx={{ marginRight: '16px' }}>
                                <Avatar>
                                    <QuizIcon />
                                </Avatar>
                            </ListItemAvatar>
                            <ListItemText primary={test.name} />
                            <ListItemText sx={{ flexGrow: 1 }}>Grade: {test.mark}%</ListItemText>
                            <ListItemAvatar sx={{ flexGrow: 1 }}>
                                <Avatar>
                                    {test.mark >= 70 ? (
                                        <CheckCircleIcon color='success'/>
                                    ) : (
                                        <CancelIcon color='error'/>
                                    )}
                                </Avatar>
                            </ListItemAvatar>
                        </ListItem>
                    ))}
                </List>
            ) : (
                <p>Loading data...</p>
            )}
        </div>
    );
};

export default PassedTests;