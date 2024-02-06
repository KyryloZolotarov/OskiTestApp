import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import ListItem from '@mui/material/ListItem';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import ListItemText from '@mui/material/ListItemText';
import Avatar from '@mui/material/Avatar';
import QuizIcon from '@mui/icons-material/Quiz';

interface TestData {
    names: {
        [key: number]: string;
    };
}

const Home = () => {
    const [testsData, setTestsData] = useState<TestData | null>(null);
    const [selectedTest, setSelectedTest] = useState<number | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        // Fetching data from the server
        fetch("http://localhost:5003/test/getAvailableTests", {
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

    const handleTestSelect = (testId: number) => {
        setSelectedTest(testId);
        navigate(`/test/${testId}`);
    };

    return (
        <div>
            <Typography variant="h5" sx={{ pl: 2, pt: 2, pb: 2 }}>Available Tests</Typography>
            {testsData ? (
                <Grid container spacing={2}>
                    {testsData &&
                        testsData.names &&
                        Object.keys(testsData.names).map((testId) => (
                            <Grid key={testId} item xs={12} md={12}>
                                <Button
                                    onClick={() => handleTestSelect(Number(testId))}
                                    variant="text"
                                    color="inherit"
                                    sx={{ width: '100%', display: 'flex', alignItems: 'center', borderTop: '1px solid #e0e0e0', borderBottom:'1px solid #e0e0e0' }}
                                >
                                    <ListItem alignItems="center">
                                        <ListItemAvatar>
                                            <Avatar>
                                                <QuizIcon />
                                            </Avatar>
                                        </ListItemAvatar>
                                        <ListItemText
                                            primary={testsData.names[+testId]}
                                            secondary={selectedTest && selectedTest === +testId ? 'Selected' : null}
                                        />
                                    </ListItem>
                                </Button>
                            </Grid>
                        ))}
                </Grid>
            ) : (
                <p>Loading data...</p>
            )}
        </div>
    );
};

export default Home;