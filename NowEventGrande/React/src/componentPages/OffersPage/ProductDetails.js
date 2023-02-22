import * as React from 'react';
import { Container } from 'react-bootstrap'
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'
import '../../styles/Offers/ProductDetail.css'
import sample from '../../images/sample.jpg';
import { Divider, Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from '@mui/material';
import agent from '../../app/api/agent';
import LoadingComponent from '../../app/layout/LoadingComponent';
export const ProductDetails = () => {

    const { id } = useParams();
    const [offer, setOffer] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent.Offers.productDetails(parseInt(id))
            .then(offer => setOffer(offer))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, [id]);

    if (loading) return <LoadingComponent message='Loading offer...' />
    console.log(offer)
    return (
        <Container className='container_offers'>

        <Grid container spacing={6} sx={{mt: 20} }>
            <Grid item xs={4}>
                <img src={sample} alt={offer.name} style={{ width: '100%' }} />
            </Grid>
            
            <Grid item xs={8}>
                <Typography variant='h4' style={{ textAlign: 'center' }}>{offer.name}</Typography>
                <Divider sx={{ mb: 2 }} />
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell>Type</TableCell>
                                <TableCell>{offer.type}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Size</TableCell>
                                <TableCell>{offer.size}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Event Start</TableCell>
                                <TableCell>{offer.eventStart}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Event End</TableCell>
                                <TableCell>{offer.eventEnd}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell>Guests</TableCell>
                                <TableCell>{offer.guests}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>
            </Grid>
        </Container>
    )
}
