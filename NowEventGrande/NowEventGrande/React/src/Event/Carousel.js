import '../styles/carousel.css';
import React from 'react';
import calendarIcon from '../images/icons/contact-list.png';
import 'react-responsive-carousel/lib/styles/carousel.min.css';
// var ReactDOM = require('react-dom');
var CarouselReact = require('react-responsive-carousel').Carousel;

function Carousel(){

    

    return(
        <div >
            <CarouselReact showArrows={true}                           
            >
                <div> 
                    <img src={calendarIcon} className="carouselImage"/>
                    <img src={calendarIcon} className="carouselLight"/>
                    <p className="legend">Legend 1</p>
                </div>
                <div>
                    <img src={calendarIcon} className="carouselImage"/>
                    <p className="legend">Legend 2</p>
                </div>
                <div>
                    <img src={calendarIcon} className="carouselImage"/>
                    <p className="legend">Legend 3</p>
                </div>
                <div>
                    <img src={calendarIcon} className="carouselImage"/>
                    <p className="legend">Legend 4</p>
                </div>
                <div>
                    <img src={calendarIcon} className="carouselImage"/>
                    <p className="legend">Legend 5</p>
                </div>
                <div>
                    <img src={calendarIcon} className="carouselImage"/>
                    <p className="legend">Legend 6</p>
                </div>
            </CarouselReact>
        </div>
    )
}

export default Carousel;