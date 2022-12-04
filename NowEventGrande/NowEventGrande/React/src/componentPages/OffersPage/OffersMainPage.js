import React from 'react'
import { Container } from 'react-bootstrap'
import { FilterSection } from './FilterSection.js'
import { Sort } from './Sort.js'
import { OffersList } from './OffersList.js'
import { async } from 'jshint/src/prod-params.js'
import { useEffect, useState } from 'react'

export const OffersMainPage = () => {
    const [offer, setOffer] = useState([]);
    useEffect(() => {
        fetchOffers();
    }, []);

    const fetchOffers = async () => {
        const data = await fetch('/offers');
        const offers = await data.json();
        console.log(offers);
        setOffer(offers.results);
    }
    return (
        <Container>
            <div className="grid grid-filter-column">
                <div>
                    <FilterSection />
                </div>

                <section className="offers-view--sort">
                    <div className="sort-filter">
                        <Sort />
                    </div>
                    <div className="main-offers">
                        <OffersList />
                    </div>
                </section>
            </div>
        </Container>
    )
}
