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
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Voluptate totam voluptates dolorum consequatur suscipit ad unde facere ratione eveniet nostrum.</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="benefit-header py-1">
                        <span>
                            <img src={benefit2} alt="img" />
                        </span>
                        <h2>Comfortable</h2>
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Voluptate totam voluptates dolorum consequatur suscipit ad unde facere ratione eveniet nostrum.</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="benefit-header py-1">
                        <span>
                            <img src={benefit1} alt="img" />
                        </span>
                        <h2>Happiness</h2>
                        <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Voluptate totam voluptates dolorum consequatur suscipit ad unde facere ratione eveniet nostrum.</p>
                    </Col>
                    <Col cs={12} md={6} xl={7} className="btn-join">
                        <Nav.Link as={Link} to="/event">
                            <button>Get Started<ArrowRightCircle size={25} /></button>
                        </Nav.Link>
                    </Col>
                </Row>
            </Container >
        </div>
    )
}
