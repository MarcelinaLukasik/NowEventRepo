import React from 'react';
import '../styles/rateSlider.css';
import {Col} from "react-bootstrap";
import Icon from '../images/icons/list.png';
import {useLocation} from 'react-router-dom';
import { useState } from "react";
import SideBar from "./SideBar";

function AfterEvent() {
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [CommunicationRate, setCommunicationRate] = useState(3);
    const [QualityRate, setQualityRate] = useState(3);
    const [SpeedRate, setSpeedRate] = useState(3);
    

    function MoveSlider(evt) {
        const slider = evt.target;
        const value = slider.value;
        const rate = slider.id;
        if (rate === "CommunicationRange"){
            setCommunicationRate(value);
        }
        else if (rate === "QualityRange"){
            setQualityRate(value);
        }
        else if (rate === "SpeedRange"){
            setSpeedRate(value);
        }
    }

    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost();
      }

    function handlePost(){
      async function fetchData() {
        const res = await fetch(`/events/SaveRatings`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({CommunicationRating: CommunicationRate, QualityRating: QualityRate , SppedRating: SpeedRate, EventId: eventId}) ,
        })
        if (!res.ok) {
          const message = `An error has occured: ${res.status} - ${res.statusText}`;
          throw new Error(message);
        }
    }  
      fetchData();    
    }

    return (
        <div className="event">
                    <div>              
                        <h1>After event</h1>
                        <div className="row">
                            <div className="Event-col-3">
                            <div className="sidebar">
                            <SideBar eventId={eventId}/>
                            </div>
                            </div>
                        <div className="Event-col-6">              
                                <br/>  
                                <h2>Please take a moment to rate your experience  
                                    <br></br>
                                     with the contractor:
                                </h2>                                       
                                <div>
                                    <h3>Communication:</h3>
                                    <div className="rateSlidecontainer">
                                        <input type="range" min="1" max="5" value={CommunicationRate} className="rateSlider" id="CommunicationRange" onInput={MoveSlider}/>
                                    </div>
                                    <h3>{CommunicationRate}</h3>                                 
                                </div>   
                                <div>
                                    <h3>Quality:</h3>
                                    <div className="rateSlidecontainer">
                                        <input type="range" min="1" max="5" value={QualityRate} className="rateSlider" id="QualityRange" onInput={MoveSlider}/>
                                    </div>
                                    <h3>{QualityRate}</h3>                                 
                                </div> 
                                <div>
                                    <h3>Speed:</h3>
                                    <div className="rateSlidecontainer">
                                        <input type="range" min="1" max="5" value={SpeedRate} className="rateSlider" id="SpeedRange" onInput={MoveSlider}/>
                                    </div>
                                    <h3>{SpeedRate}</h3>                                 
                                </div> 
                                <div>
                                <form onSubmit={handleSubmit}>
                                    <input className="saveGuest" type="submit" value="Send ratings"/>
                                </form>
                                </div>                  
                        </div> 
                        <div className="Event-col-2">
                            <Col cs={12} md={6} xl={6}>
                                <img src={Icon} alt="img" className="featureIcon"/>
                            </Col>
                            </div>                  
                        </div>                             
                    </div>
                </div>

    )
}

export default AfterEvent;                