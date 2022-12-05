import React from 'react'
import { Container } from 'react-bootstrap'
import { FilterSection } from './FilterSection.js'
import { Sort } from './Sort.js'
import { OffersList } from './OffersList.js'
import { useEffect, useState } from 'react'
import '../../styles/Offers/OffersMainPage.css'
import { motion, AnimatePresence } from 'framer-motion'

export const OffersMainPage = () => {
    const [offer, setOffer] = useState([]);
    const [filtered, setFiltered] = useState([]);
    const [activeType, setActiveType] = useState("");
    useEffect(() => {
        fetchOffers();
    }, []);

    const fetchOffers = async () => {
        const data = await fetch('/offer/GetOffersWithInCompleteStatus');
        const offers = await data.json();
        console.log(offers);
        setOffer(offers);
        setFiltered(offers);
    }
    return (
        <div className="offers">
            <Container className='container_offers'>

                <FilterSection offer={offer} setFiltered={setFiltered} activeType={activeType} setActiveType={setActiveType} />
                <motion.div layout className="main-offers">
                    <AnimatePresence>
                        {filtered.map((offer) => {
                            return <OffersList key={offer.id} offer={offer} />;
                        })}
                    </AnimatePresence>
                </motion.div>
            </Container>
        </div>
    )
}
