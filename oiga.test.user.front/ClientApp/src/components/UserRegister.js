import React, { useState } from 'react';
import axios from 'axios';

export const UserRegister = ({ searchUsers }) => {
    const [txtFullName, setFullNameState] = useState('');
    const [txtUserName, setUserNameState] = useState('');
    const [txtPhone, setPhoneState] = useState('');

    const onClickRegister = (ev) => {
        if (txtFullName.trim() === '') {
            alert('Digit full name');
            return;
        }
        if (txtUserName.trim() === '') {
            alert('Digit user name');
            return;
        }

        const user = {
            fullName: txtFullName,
            userName: txtUserName,
            phone: txtPhone
        };

        axios.post(`user/register`, user).then((res) => {
            if (res.data === '') {
                setTimeout(() => {
                    searchUsers();
                    setFullNameState('');
                    setUserNameState('');
                    setPhoneState('');
                }, 200);
            } else {
                alert(res.data);
            }
        });
    };

    const onChangeFullName = (ev) => {
        setFullNameState(ev.target.value);
    };

    const onChangeUserName = (ev) => {
        setUserNameState(ev.target.value);
    };

    const onChangePhone = (ev) => {
        setPhoneState(ev.target.value);
    };

    return (
        <>
            <h4 id='tabelLabel'>Register</h4>
            <input type='text' placeholder='Full name' onChange={onChangeFullName} value={txtFullName} />
            <input type='text' placeholder='User name' onChange={onChangeUserName} value={txtUserName} />
            <input type='text' placeholder='Phone' onChange={onChangePhone} value={txtPhone} />
            <button type='button' onClick={onClickRegister}>
                Save
            </button>
        </>
    );
};

export default UserRegister;
