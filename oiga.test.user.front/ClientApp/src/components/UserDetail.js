import React, { useState, useEffect } from 'react';
import axios from 'axios';

export const UserDetail = ({ userId }) => {
    const [user, setUserState] = useState();

    useEffect(() => {
        if (userId) {
            axios.get(`user/getbyid?id=${userId}`).then((response) => {
                setUserState(response.data);
            });
        }
    }, [userId]);

    return (
        <>
            <h4>Detail</h4>
            Id: {user && user.id}
            <br />
            Full Name: {user && user.fullName}
            <br />
            User Name: {user && user.userName}
            <br />
            Phone: {user && user.phone}
        </>
    );
};

export default UserDetail;
