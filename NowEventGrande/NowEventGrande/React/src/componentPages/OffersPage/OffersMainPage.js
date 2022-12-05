import React from 'react'
import { Container } from 'react-bootstrap'
import { FilterSection } from './FilterSection.js'
import { Sort } from './Sort.js'
import { OffersList } from './OffersList.js'
import { useEffect, useState } from 'react'
import '../../styles/Offers/OffersMainPage.css'
import { motion, AnimatePresence } from 'framer-motion'
import { useSearchParams, useLocation } from 'react-router-dom'

export const OffersMainPage = () => {
    const [offer, setOffer] = useState([]);
    const [filtered, setFiltered] = useState([]);
    const [activeType, setActiveType] = useState("");
    const [searchPhrase, setSearchPhrase] = useSearchParams();
    const [query, setQuery] = useState(searchPhrase.get('query'));
    // const page = searchPhrase.get('page') || 0;
    const search = useLocation().search;
    const name = new URLSearchParams(search).get('query');
    console.log(name);
    useEffect(() => {
        fetchOffers();
    }, [query]);

    const fetchOffers = async () => {
        const data = await fetch(query ? `/offer/GetOffersWithInCompleteStatus/?query=${query}` : `/offer/GetOffersWithInCompleteStatus`);
        //const data = await fetch(`/offer/GetOffersWithInCompleteStatus`);
        const offers = await data.json();
        console.log(offers);
        setOffer(offers);
        setFiltered(offers);
    }

    const SearchChange = (e) => {
        const newQuery = e.target.value;
        setQuery(newQuery);
        console.log(newQuery);
        setSearchPhrase({
            query: newQuery,
            //page: 0
        })
    }
    return (
        <div className="offers">
            <Container className='container_offers'>
                <div className="search">
                    <div className="search_inputs">
                        <input value={query} type="text" placeholder='search...' onChange={SearchChange} />
                    </div>
                </div>
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
