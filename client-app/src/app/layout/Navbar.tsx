import React from 'react';
import { Container, Menu } from 'semantic-ui-react';

export default (function NavBar() {  
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item>
                    <img src="/assets/logo.png" alt="logo" style={{marginRight: '10px'}} />
                    Xtramile
                </Menu.Item>
            </Container>
        </Menu>
    )
})