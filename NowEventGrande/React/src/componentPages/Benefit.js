import React from 'react'
import { ArrowRightCircle } from "react-bootstrap-icons";
import { Container, Col, Row, Nav } from "react-bootstrap"
import { Link } from "react-router-dom";
import benefit1 from '../images/benefit1.svg'
import benefit2 from '../images/benefit2.svg'
import benefit3 from '../images/benefit3.svg'
import '../styles/benefit.css';




export const Benefit = () => {
    return (
        <div className='bg-light wrap'>
            <Container>
                <h1 className='header-title'>Benefits</h1>
                <Row className="benefit">
                    <Col cs={12} md={6} xl={7} className="benefit-header py-1">
                        <span>
                            <img src={benefit3} alt="img" />
                        </span>
                        <h2>Time saving</h2>
                        <p>NowEvent offers many features, but don't worry. Creating a new event is quick and uncomplicated.</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="benefit-header py-1">
                        <span>
                            <img src={benefit2} alt="img" />
                        </span>
                        <h2>Comfortable</h2>
                        <p>You can create your dream event without leaving home, and let someone else do all the job.</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="benefit-header py-1">
                        <span>
                            <img src={benefit1} alt="img" />
                        </span>
                        <h2>Happiness</h2>
                        <p>You can have fun without the stress of planning and organizing your event. Finally!</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="btn-join">
                        <Nav.Link as={Link} to="/event">
                            <button className='createEventButton'>Get Started<ArrowRightCircle size={25} /></button>
                        </Nav.Link>
                    </Col>
                </Row>
            </Container >
        </div>
    )
}
