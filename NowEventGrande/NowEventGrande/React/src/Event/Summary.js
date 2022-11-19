import SideBar from "./SideBar";
import React from 'react';
import '../styles/guests.css';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/find.png';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import { Outlet, NavLink } from "react-router-dom";
import {handleStyle} from "./HandleProgress";
import Counter from 'react-countdown-customizable';

function Summary() {
    const [isOpen, setOpen] = useState(false);
    const openInput = () => {setOpen(!isOpen);}
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [count, setCount] = useState(0);
    const [eventTime, setEventTime] = useState();
    const [type, setType] = useState();
    const [title, setTitle] = useState();
    const Completionist = () => <span>Time to party!</span>;
    const [status, setStatus] = useState(false);

    useEffect(() => {
        handleEventStartTime();  
        fetchProgress();      
        checkStatus().then(() => {getInfo();}); 
    }, []);

    useEffect(() => {
        handleStyle(count);
    }, [count]);  

    async function handleEventStartTime()
    {
        var result = await GetEventStartTime();
        var startDate = await result.text();
        setEventTime(startDate);
    }

    async function GetEventStartTime(){
        const res = await fetch(`/events/${eventId}/GetEventStartDate`);
        return res;
    }

    async function fetchProgress() {
      const res = await fetch(`/events/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res));
    }

    async function checkStatus() {
        const res = await fetch(`/events/${id}/CheckStatus`);
    }  

    async function getInfo() {
        const res = await fetch(`/events/${id}/GetEventInfo`);      
        const result = await res.json();
        setType(result.Type);
        setTitle(result.Name);
        setStatus(result.Status);
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
                <h1>Summary</h1>
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
                   <div className="Event-col-6">              
                        <br/>   
                        <h3>Title: {title}</h3>                                      
                        {eventTime && <Counter className="countdown" date={eventTime}
                        timerStyle={{ margin: "auto", width: "45%", fontSize: "25px"}}
                        labelStyle={{
                          color: "#b1b1b1",
                          fontSize: "14px",
                          textTransform: "uppercase",
                          marginTop: "12px"      
                        }}                            
                        >
                            <Completionist />
                        </Counter>  } 
                        <div className="summaryStats">
                        <p>Current status: {status}</p> 
                        <p>Your event type: {type}</p>
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

export default Summary;