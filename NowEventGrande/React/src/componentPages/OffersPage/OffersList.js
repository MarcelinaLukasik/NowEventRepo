import React, { Component } from 'react';
import sample from '../../images/sample.jpg';
import { motion } from 'framer-motion';
import { Link } from 'react-router-dom';
export const OffersList = ({ offer }) => {

    return (
        <motion.div animate={{ opacity: 1 }} initial={{ opacity: 0 }} exit={{ opacity: 0 }} layout className='offer_tile'>
            <h2>{offer.name}</h2>
            <img src={sample} alt="sample img" />
            <div className='offer_desc'>
                <span className='offer_size'>Size: {offer.size}</span>
                <span className='offer_type'>Type: {offer.type}</span>
            </div>
            {/* TODO: Cut date on backend side */}
            <h3>Start: {offer.date.split("T")[0]}</h3>
            <Link to={`/offer/${offer.id}`} className="button">More Info</Link>

        </motion.div>
    )
}
