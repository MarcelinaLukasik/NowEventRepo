import React from 'react'
import { useEffect } from 'react'
import { Container } from 'react-bootstrap'
export const FilterSection = ({ offer, setFiltered, activeType, setActiveType }) => {
    console.log(offer);
    useEffect(() => {
        if (activeType === "") {
            setFiltered(offer);
            return;
        }
        const filtered = offer.filter((offer) => offer.type.includes(activeType));
        setFiltered(filtered);
    }, [activeType])
    return (
        <div className="filters">
            <button className={activeType === "" ? "active" : ""} onClick={() => setActiveType("")}>All</button>
            <button className={activeType === "Birthday" ? "active" : ""} onClick={() => setActiveType("Birthday")}>Birthday</button>
            <button className={activeType === "Festival" ? "active" : ""} onClick={() => setActiveType("Festival")}>Festival</button>
            <button className={activeType === "Concert" ? "active" : ""} onClick={() => setActiveType("Concert")}>Concert</button>
        </div>
    )
}
