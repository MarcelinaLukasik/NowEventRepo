import { Container, Row, Col, Tab, Nav } from "react-bootstrap";
import { FeatureCard } from "./FeatureCard";
import projImg1 from "../images/budget.jpg";
import projImg2 from "../images/guest-list.jpg";
import projImg3 from "../images/date-time.jpg";
import projImg4 from "../images/inspiration.jpg";
import colorSharp2 from "../images/benefit1.svg";
import '../styles/features.css';
import TrackVisibility from 'react-on-screen';

export const Features = () => {

    const features = [
        {
            title: "Guests",
            description: "Manage your guest list",
            imgUrl: projImg2,
        },
        {
            title: "Where and when?",
            description: "Set the date and place for your event",
            imgUrl: projImg3,
        },
        {
            title: "Theme",
            description: "Something oldschool? Something modern? Your choice!",
            imgUrl: projImg2,
        },
        {
            title: "Budget",
            description: "Keep an eye on your expenses",
            imgUrl: projImg3,
        },
        {
            title: "Summary",
            description: "All the important information in one place",
            imgUrl: projImg2,
        },
        {
            title: "Your opinion",
            description: "Rate your experience and help other with their choice",
            imgUrl: projImg3,
        },
    ];

    return (
        <section className="features" id="features">
            <Container>
                <Row>
                    <Col size={12}>
                        <TrackVisibility>
                            {({ isVisible }) =>
                                <div className={isVisible ? "animate__animated animate__fadeIn" : ""}>
                                    <h2>Features</h2>
                                    <p>Here are a bunch of features that will help you with planning your party:</p>
                                    <Tab.Container id="features-tabs" defaultActiveKey="first">
                                        <Nav variant="pills" className="nav-pills mb-5 justify-content-center align-items-center" id="pills-tab">
                                            <Nav.Item>
                                                <Nav.Link eventKey="first">Create event</Nav.Link>
                                            </Nav.Item>
                                            <Nav.Item>
                                                <Nav.Link eventKey="second">Post offer</Nav.Link>
                                            </Nav.Item>
                                            <Nav.Item>
                                                <Nav.Link eventKey="third">Inspirations</Nav.Link>
                                            </Nav.Item>
                                        </Nav>
                                        <Tab.Content id="slideInUp" className={isVisible ? "animate__animated animate__slideInUp" : ""}>
                                            <Tab.Pane eventKey="first">
                                                <Row>
                                                    {
                                                        features.map((project, index) => {
                                                            return (
                                                                <FeatureCard
                                                                    key={index}
                                                                    {...project}
                                                                />
                                                            )
                                                        })
                                                    }
                                                </Row>
                                            </Tab.Pane>
                                            <Tab.Pane eventKey="second">
                                                <p>After you complete your event settings you can post it on our board and wait for contractors offers. It is really that simple.</p>
                                            </Tab.Pane>
                                            <Tab.Pane eventKey="third">
                                                <p>If you want to make your event look good, don't forget to check out Inspiration section with ideas and 
                                                    cool decorations for events!
                                                </p>
                                            </Tab.Pane>
                                        </Tab.Content>
                                    </Tab.Container>
                                </div>}
                        </TrackVisibility>
                    </Col>
                </Row>
            </Container>
        </section>
    )
}