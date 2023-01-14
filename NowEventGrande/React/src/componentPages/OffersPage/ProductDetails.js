import * as React from 'react';
import { Link, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react'
import '../../styles/Offers/ProductDetail.css'
import { Container } from 'react-bootstrap'
import sample from '../../images/sample.jpg';

export const ProductDetails = () => {
    let { offerId } = useParams();
    const [offer, setOffer] = useState([]);

    useEffect(() => {
        fetchOffer();
    }, []);

    const fetchOffer = async () => {
        const data = await fetch(`/offer/singleOffer/${offerId}`);
        const offer = await data.json();
        console.log(offer);
        setOffer(offer);

    }
    return (
        <div className="detail-offer">
            <Container>
                <div className="detail-container">
                    <div className="left-side">
                        <div className="offer-tile-left">
                            <img src={sample} alt="sample img" />         
                        </div>
                    </div>
                    <div className="right-side">
                        <div className="offer-tile-right">
                            <h2>{offer.name}</h2>
                            <h3>Date: {offer.date?.slice("T",-1)}</h3>
                            <h3>Event Start: {offer.eventStart?.slice("T",-1)}</h3>
                            <h3>Event End: {offer.eventEnd?.slice("T",-1)}</h3>
                            <h3>Event localization: {offer.eventAddresses}</h3>

                            <div className='offer_desc-detail'>
                                <span className='offer_size'>Size: {offer.size}</span>
                                <span className='offer_type'>Type: {offer.type}</span>
                            </div>
                            
                            {console.log("Im here")}
                            <div className='offer_desc-detail'>
                                <span className='offer_size'>Guests: {offer.guests}</span>
                                <span className='offer_type'>Budget: {offer.budget}</span>
                            </div>
                        </div>
                    </div>
                    <Link to={`/singleoffer/${offerId}/contact`} className="button-contact">Contact</Link>
                </div>
            </Container>
        </div>
    )
}
