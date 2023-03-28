import sample from '../../images/sample.jpg';
import { motion } from 'framer-motion';
import { Avatar, Button, Card, CardActions, CardContent, CardHeader, CardMedia, Typography, Box } from '@mui/material';
import { Link } from 'react-router-dom';
import { Nav } from "react-bootstrap";

export const OffersList = ({ offer }) => {
    return (
        <motion.div animate={{ opacity: 1 }} initial={{ opacity: 0 }} exit={{ opacity: 0 }} layout className='offer_tile'>
            <Card>
                <CardHeader
                    avatar={
                        <Avatar sx={{ bgColor: 'grey.500' }}>
                            {offer.type.charAt(0).toUpperCase()}
                        </Avatar>
                    }
                    title={offer.name}
                    titleTypographyProps={{
                        sx: { fontWeight: 'bold', color: 'primary.main' }
                        } }
                />
                <CardMedia
                    sx={{ height: 140, width: '100%', backgroundSize: 'contain', bgcolor: 'text.primary' }}
                    image={sample}
                    title={offer.name}
                />
                <CardContent>
                    <Box style={{ textAlign: 'center' }}>
                        <Typography gutterBottom color='secondary' variant="h5">
                            {offer.eventStart.split("T")[0]}
                        </Typography>
                        <Typography variant="body2" color="text.primary">
                            {offer.size} / {offer.type}
                        </Typography>
                    </Box>
                    
                </CardContent>
                <CardActions>
                    <Nav.Link as={Link} to={`/alloffers/${offer.id}`}>
                            <button className="saveDate">View</button>
                    </Nav.Link>
                </CardActions>
            </Card>
        </motion.div>
    )
}
