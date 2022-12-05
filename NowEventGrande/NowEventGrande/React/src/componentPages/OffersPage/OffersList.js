import React from 'react'
import sample from '../../images/sample.jpg'
import { motion } from 'framer-motion'
export const OffersList = ({ offer }) => {
    return (
        <motion.div animate={{ opacity: 1 }} initial={{ opacity: 0 }} exit={{ opacity: 0 }} layout className='offer_tile'>
            <h2>{offer.name}</h2>
            <img src={sample} alt="sample img" />
            <div>
                <span>{offer.size}</span>
                <span>{offer.type}</span>
            </div>
            <h3>{offer.start}</h3>
            <button>more...</button>

        </motion.div>
    )
}
