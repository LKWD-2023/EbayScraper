import React, { useState } from 'react';
import axios from 'axios';

const App = () => {

    const [searchTerm, setSearchTerm] = useState('');
    const [items, setItems] = useState([]);

    const onSearchClick = async () => {
        const { data } = await axios.get(`/api/ebayscraper/scrape?searchTerm=${searchTerm}`);
        setItems(data);
    }

    return (
        <div className='container mt-5'>
            <div className='row'>
                <div className='col-md-10'>
                    <input value={searchTerm} onChange={e => setSearchTerm(e.target.value)} className='form-control input-lg' />
                </div>
                <div className='col-md-2'>
                    <button onClick={onSearchClick} className='btn btn-primary w-100'>Search</button>
                </div>
            </div>

            <div className='row mt-3'>
                <table className='table table-bordered'>
                    <thead>
                        <tr>
                            <th>Image</th>
                            <th>Title</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item => {
                            return <tr key={item.url}>
                                <td><img src={item.image} style={{width:200}} /></td>
                                <td>
                                    <a target="_blank" href={item.url}>{item.title}</a>
                                </td>
                                <td>
                                    {item.price}
                                </td>
                            </tr>
                        })}
                    </tbody>
                </table>
            </div>
        </div>
    )

}

export default App;