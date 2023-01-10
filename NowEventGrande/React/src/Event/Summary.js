import SideBar from "./SideBar";
import Assistance from "./Assistance";
import React from 'react';
import '../styles/guests.css';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import {handleStyle} from "./HandleProgress";
import Counter from 'react-countdown';


function Summary() {   
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
      const res = await fetch(`/progress/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => setCount(res));
    }

    async function checkStatus() {
        const res = await fetch(`/progress/${id}/CheckStatus`);
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
                        <SideBar eventId={eventId}/>
                    </div>
                    </div>
                    <div className="Event-col-1"> 
                   
                   </div>   
                   <div className="Event-col-4">              
                        <br/>   
                        <h2>Title: {title}</h2>                                      
                        {eventTime && <Counter className="countdown" date={eventTime}
                        timerStyle={{ margin: "auto", width: "60%", fontSize: "25px"}}
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
                        <h3>Current status: {status}</h3> 
                        <h3>Your event type: {type}</h3>
                        </div>  
                              
                   </div>   
                   <div className="Event-col-3">
                    {/* <Col cs={12} md={6} xl={6}>
                        <img src={lighbulbIcon} alt="img" className="lightbulbIcon"/>
                    </Col> */}
                        <div>
                            <Assistance eventId={eventId}/>   
                        </div>       
                    </div> 
                    <div className="Event-col-1"> 
                   
                    </div>      
                </div>                             
            </div>
        </div>
)
}

export default Summary;