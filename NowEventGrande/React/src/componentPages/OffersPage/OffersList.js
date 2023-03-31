import sample from '../../images/sample.jpg';
import formalImage from '../../images/themes/formal.jpg';
import spookyImage from '../../images/themes/spooky.jpg';
import tropicalImage from '../../images/themes/tropical.jpg';
import retroImage from '../../images/themes/retro.jpg';
import discoImage from '../../images/themes/disco.jpg';
import otherImage from '../../images/themes/other.jpg';
import { motion } from 'framer-motion';
import { Avatar, Card, CardActions, CardContent, CardHeader, CardMedia, Typography, Box } from '@mui/material';
import { Link } from 'react-router-dom';
import { Nav } from "react-bootstrap";
import { useState, useEffect } from "react";

export const OffersList = ({ offer }) => {
    const [offerImage, setOfferImage] = useState("");

    useEffect(() => {
        GetThemeImage();      
    } ,[])

    async function GetThemeImage() {
        switch(offer.theme) {
            case "Formal":
                setOfferImage(formalImage);
                break;
            case "Spooky":
                setOfferImage(spookyImage);
                break;
            case "Tropical":
                setOfferImage(tropicalImage);
                break;
            case "Retro":
                setOfferImage(retroImage);
                break;
            case "Disco":
                setOfferImage(discoImage);
                break;
            case "Other":
                setOfferImage(otherImage);
                break;
            default:
                setOfferImage(sample);
          }
    }


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
                    image={offerImage}
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
