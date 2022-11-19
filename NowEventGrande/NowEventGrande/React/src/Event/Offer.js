import React from 'react';
import '../styles/guests.css';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/find.png';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import { Outlet, NavLink } from "react-router-dom";

function Offer() {
    const [isOpen, setOpen] = useState(false);
    const openInput = () => {setOpen(!isOpen);}
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [status, setStatus] = useState(false);

    useEffect(() => {
        async function fetchData() {
          const res = await fetch(`/events/${id}/CheckStatus`);      
          res
            .json()
            .then(res => setStatus(res))
        }
         fetchData();  
    }, []);

    function handlePostOffer()
    {
        // not impletemted yet
    }

    return (
        <div className="event">
                    <div>              
                        <h1>Post your offer</h1>
                        
                        <div className="row">                     
                            <div className="Event-col-3">
                            <div className="sidebar">
                                <div className="vertical-menu">
                                <ul>
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/guests`}} state={{EventId: id}}>Guest list</NavLink>
                                    </li>   
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/budget`}} state={{EventId: id}} >Budget</NavLink>
                                    </li>
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/location`}} state={{EventId: id}} >Location and date</NavLink>
                                    </li>
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/offer`}} state={{EventId: id}} >Make offer</NavLink>
                                    </li>
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/afterEvent`}} state={{EventId: id}} >After event</NavLink>
                                    </li>
                                    <li >
                                        <NavLink to={{pathname :`/event/${id}/summary`}} state={{EventId: id}} >Summary</NavLink>
                                    </li>
                                </ul>
                                <Outlet />
                                </div>
                            </div>
                            </div>
                        <div  className="Event-col-6">
                            {status &&
                            <div>
                            <h3 className="offerInformation">Good job, your event checklist is now completed. 
                            Make sure all data is correct before you post your offer.</h3>
                            <input type="button" className="postOffer" value="Post my offer" onClick={handlePostOffer}/>
                            </div>
                            }
                            {!status &&
                            <h3 className="offerInformation">Your event is not completed yet. Provide all required information and come back!.</h3>
                            }
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

export default Offer;   