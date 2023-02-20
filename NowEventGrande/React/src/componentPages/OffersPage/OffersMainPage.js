import React from 'react'
import ReactPaginate from "react-paginate";
import { Container } from 'react-bootstrap'
import { FilterSection } from './FilterSection.js'
import { Pagination } from './Pagination.js'
import { OffersList } from './OffersList.js'
import { useEffect, useState } from 'react'
import '../../styles/Offers/OffersMainPage.css'
import { motion, AnimatePresence } from 'framer-motion'
import { useSearchParams } from 'react-router-dom'
import agent from '../../app/api/agent.js';

export const OffersMainPage = () => {
    const [offer, setOffer] = useState([]);
    const [offers, setOffers] = useState([]);
    const [filtered, setFiltered] = useState([]);
    const [activeType, setActiveType] = useState("");
    const [searchPhrase, setSearchPhrase] = useSearchParams();
    const [query, setQuery] = useState(searchPhrase.get('query'));
    const [currentPage, setCurrentPage] = useState([1]);


    useEffect(() => {
        agent.Offers.productsList()
            .then(offers => setOffers(offers))
            .catch(error => console.log(error))
    }, []);

    ////const fetchOffers = async () => {
    ////    const data = await fetch(query ? `/offer?searchPhrase=${query}&pageSize=10&pageNumber=${currentPage}&sortBy=Name&sortDirection=DESC`
    ////        : `/offer?searchPhrase=&pageSize=10&pageNumber=${currentPage}&sortBy=Name&sortDirection=DESC`);
    ////    const offers = await data.json();
    ////    console.log(offers);
    ////    setOffer(offers.items);
    ////    setFiltered(offers.items);
    ////    setAllOffers(offers);
    ////    setSearchPhrase({
    ////        query: query == null ? "" : query,
    ////        pageSize: 10,
    ////        pageNumber: currentPage == null ? 1 : currentPage,
    ////        SortBy: "Name",
    ////        sortDirection: 'ASC'
    ////        //page: 0s
    ////    })
    ////}

    //const SearchChange = (e) => {
    //    const newQuery = e.target.value;
    //    setQuery(newQuery);
    //}

    //const fetchOffersByCurrentPage = async (currentPage) => {
    //    const res = await fetch(query ? `/offer?searchPhrase=${query}&pageSize=10&pageNumber=${currentPage}&sortBy=Name&sortDirection=ASC`
    //        : `/offer?searchPhrase=&pageSize=10&pageNumber=${currentPage}&sortBy=Name&sortDirection=ASC`);
    //    const data = await res.json();
    //    return data;
    //}

    //const handlePageClick = async (data) => {
    //    console.log(data.selected)
    //    let currPage = data.selected + 1;
    //    setCurrentPage(currPage);
    //    const offersFromServer = await fetchOffersByCurrentPage(currPage);
    //    console.log(offersFromServer);
    //    setOffer(offersFromServer.items);
    //    setFiltered(offersFromServer.items);
    //    setSearchPhrase({
    //        query: '',
    //        pageSize: 10,
    //        pageNumber: currPage,
    //        SortBy: "Name",
    //        sortDirection: 'ASC'
    //        //page: 0s

    //    })
    //}

    return (
        <div className="offers">
            <Container className='container_offers'>
                <div className="search">
                    {/*<div className="search_inputs">*/}
                    {/*    <input value={query === null ? "" : query} type="text" placeholder='search...'/>*/}
                    {/*</div>*/}
                </div>
                <FilterSection offer={offers} setFiltered={setFiltered} activeType={activeType} setActiveType={setActiveType} />
                <motion.div layout className="main-offers">
                    <AnimatePresence>
                        {offers.map((offer) => {
                            return <OffersList key={offer.id} offer={offer} />;
                        })}
                    </AnimatePresence>
                </motion.div>
                {/*<ReactPaginate*/}
                {/*    breakLabel={"..."}*/}
                {/*    nextLabel={"next >"}*/}
                {/*    pageCount={alloffers.totalPages}*/}
                {/*    previousLabel={"< previous"}*/}
                {/*    marginPagesDisplayed={2}*/}
                {/*    onPageChange={handlePageClick}*/}
                {/*    containerClassName={'pagination justify-content-center'}*/}
                {/*    pageClassName={'page-item'}*/}
                {/*    pageLinkClassName={'page-link'}*/}
                {/*    previousClassName={'page-item'}*/}
                {/*    previousLinkClassName={'page-link'}*/}
                {/*    nextClassName={'page-item'}*/}
                {/*    nextLinkClassName={'page-link'}*/}
                {/*    breakClassName={'page-item'}*/}
                {/*    breakLinkClassName={'page-link'}*/}
                {/*    activeClassName={'active'}*/}
                {/*/>*/}
            </Container>
        </div>
    )
}
