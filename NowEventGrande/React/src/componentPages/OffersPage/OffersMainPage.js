import React from 'react'
import { Container } from 'react-bootstrap'
import { OffersList } from './OffersList.js'
import { useEffect, useState } from 'react'
import '../../styles/Offers/OffersMainPage.css'
import { motion, AnimatePresence } from 'framer-motion'
import agent from '../../app/api/agent.js';
import LoadingComponent from '../../app/layout/LoadingComponent.jsx'

export const OffersMainPage = () => {
    const [offers, setOffers] = useState([]);
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        agent.Offers.productsList()
            .then(offers => setOffers(offers))
            .catch(error => console.log(error))
            .finally(() => setLoading(false))
    }, []);
    
    if (loading) return <LoadingComponent message='Loading products...' />

    return (
        <div className="offers">
            <Container className='container_offers'>
                <div className="search">
                </div>
                <motion.div layout className="main-offers">
                    <AnimatePresence>
                        {console.log(offers)}
                        {offers.map((offer) => {
                            return <OffersList key={offer.id} offer={offer} />;
                        })}
                    </AnimatePresence>
                </motion.div>
            </Container>
        </div>
    )
}
