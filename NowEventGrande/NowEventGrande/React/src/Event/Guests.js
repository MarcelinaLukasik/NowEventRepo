import SideBar from "./SideBar";
import React from 'react';
import AllGuests from "./AllGuests";
import '../styles/guests.css';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/contact-list.png';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import {handleStyle} from "./HandleProgress";

function Guests() {
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [count, setCount] = useState(0);

    useEffect(() =>{  
        fetchProgress();
    }, [])

    useEffect(() => {
      handleStyle(count);
    }, [count]);

    async function fetchProgress() {
      const res = await fetch(`/events/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res));
    }
    

    return (
        <div className="event">
            <div>  
                <div className="row">
                    <div className="Event-col-12">    
                        <div className="progressBarContainer"> 
                            <h3 className="progressText" >Checklist progress:</h3>  
                                <div className="progress" id="progress">
                                    <div className="progress-bar" id="progress-bar"></div>
                                </div> 
                        </div>
                    </div>
                </div>     
                <h1>Guests list</h1>
                <div className="row">
                    <div className="Event-col-3">
                    <div className="sidebar">
                        <SideBar eventId={eventId}/>
                    </div>
                    </div>        
                   <div className="Event-col-6 guestList">              
                        <br/>                                         
                        <div className="guestList">
                            <AllGuests eventId={id}/>
                        </div>                       
                   </div>
                   <div className="Event-col-2">
                    <Col cs={12} md={6} xl={6}>
                        <img src={calendarIcon} alt="img" className="featureIcon"/>
                    </Col>
                    </div>                  
                </div>                             
            </div>
        </div>
)
}

export default Guests;