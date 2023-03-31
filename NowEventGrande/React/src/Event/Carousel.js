import '../styles/carousel.css';
import React from 'react';
import formalImage from '../images/themes/formal.jpg';
import spookyImage from '../images/themes/spooky.jpg';
import tropicalImage from '../images/themes/tropical.jpg';
import retorImage from '../images/themes/retro.jpg';
import discoImage from '../images/themes/disco.jpg';
import otherImage from '../images/themes/other.jpg';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
import { useLocation} from 'react-router-dom';
import { useState } from "react";
const CarouselReact = require('react-responsive-carousel').Carousel;

function Carousel(){
    const location = useLocation();
    const eventId = location.state.EventId;
    const [theme, setTheme] = useState("");


    async function saveTheme(event){
        var selectedTheme = event.target.value;
        setTheme(selectedTheme);
        const res = await fetch(`/events/${eventId}/SetTheme`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "x-access-token": "token-value",
        },
        body: JSON.stringify(selectedTheme),
        })
        
        if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;          
            throw new Error(message);
            } 
    }

    return(
        <div >
            <h3>Choose your theme:</h3>
            <CarouselReact showArrows={true}  
            autoPlay={true} 
            infiniteLoop={true}                        
            >
                <div> 
                    <img src={formalImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={formalImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Formal" type="button" onClick={saveTheme}/>
                </div>
                <div>
                    <img src={spookyImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={spookyImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Spooky" type="button" onClick={saveTheme}/>
                </div>
                <div>
                    <img src={retorImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={retorImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Retro" type="button" onClick={saveTheme}/>
                </div>
                <div>
                    <img src={tropicalImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={tropicalImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Tropical" type="button" onClick={saveTheme}/>
                </div>
                <div>
                    <img src={discoImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={discoImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Disco" type="button" onClick={saveTheme}/>
                </div>
                <div>
                    <img src={otherImage} className="carouselImage" loading="lazy" alt="oops"/>
                    <img src={otherImage} className="carouselLight" loading="lazy" alt="oops"/>
                    <input className="legend" value="Other" type="button" onClick={saveTheme}/>
                </div>
            </CarouselReact>
            <h3>Selected theme: {theme}</h3>
        </div>
    )
}

export default Carousel;