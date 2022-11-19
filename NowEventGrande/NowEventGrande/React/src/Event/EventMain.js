import SideBar from "./SideBar";
import React from 'react';
import AddGuestForm from "./AddGuestForm";
import AllGuests from "./AllGuests";
import '../styles/guests.css';
import {useLocation} from 'react-router-dom';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/list.png';
import { Outlet, NavLink } from "react-router-dom";
import { useState, createContext, useEffect } from "react";

const EventIdContext = createContext();

  
function EventMain() {
    const location = useLocation();
    const id = location.state.eventId;


    return (
        <div className="event">  
            <div>         

                <h1>New event</h1>
                <div className="row">
                    <div className="Event-col-3">
                    <EventIdContext.Provider value={id}>
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
                    </EventIdContext.Provider>
                    </div>
                    
                    <div className="Event-col-5">              
                        <br/>     
                        <h3>Welcome to your new event! You can start planning by choosing one of the options in the menu.</h3>
                   </div>
                   <div className="Event-col-2">
                    <Col cs={12} md={6} xl={6}>
                        <img src={calendarIcon} alt="img" className="calendar"/>
                    </Col>
                    </div>
                </div>                             
            </div>
        </div>
)
}

export default EventMain;
export {EventIdContext};
