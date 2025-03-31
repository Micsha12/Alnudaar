import React, { useState, useEffect } from 'react';
import './BlockRules.css';
import { useAuth } from './AuthContext';

function BlockRules() {
    const { auth } = useAuth();
    const [rules, setRules] = useState([]);
    const [newRule, setNewRule] = useState({ type: 'website', value: '', timeRange: '' });

    useEffect(() => {
        // Fetch existing blocking rules
        const fetchRules = async () => {
            const response = await fetch(`https://localhost:7200/api/blockrules/user/${auth.user.userID}`);
            if (response.ok) {
                const data = await response.json();
                setRules(data);
            } else {
                console.error('Failed to fetch blocking rules');
            }
        };

        fetchRules();
    }, [auth.user.userID]);

    const handleAddRule = async () => {
        const response = await fetch('https://localhost:7200/api/blockrules', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ ...newRule, userID: auth.user.userID }),
        });

        if (response.ok) {
            const addedRule = await response.json();
            setRules([...rules, addedRule]);
            setNewRule({ type: 'website', value: '', timeRange: '' });
        } else {
            console.error('Failed to add blocking rule');
        }
    };

    const handleDeleteRule = async (ruleID) => {
        const response = await fetch(`https://localhost:7200/api/blockrules/${ruleID}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            setRules(rules.filter((rule) => rule.blockRuleID !== ruleID));
        } else {
            console.error('Failed to delete blocking rule');
        }
    };

    return (
        <div className="block-rules-container">
            <h1>Manage Blocking Rules</h1>
            <div className="add-rule">
                <select
                    value={newRule.type}
                    onChange={(e) => setNewRule({ ...newRule, type: e.target.value })}
                >
                    <option value="website">Website</option>
                    <option value="application">Application</option>
                </select>
                <input
                    type="text"
                    placeholder="Enter website or application name"
                    value={newRule.value}
                    onChange={(e) => setNewRule({ ...newRule, value: e.target.value })}
                />
                <input
                    type="text"
                    placeholder="Time range (e.g., 09:00-17:00)"
                    value={newRule.timeRange}
                    onChange={(e) => setNewRule({ ...newRule, timeRange: e.target.value })}
                />
                <button onClick={handleAddRule}>Add Rule</button>
            </div>
            <ul>
                {rules.map((rule) => (
                    <li key={rule.blockRuleID}>
                        {rule.type === 'website' ? 'Website' : 'Application'}: {rule.value} ({rule.timeRange})
                        <button onClick={() => handleDeleteRule(rule.blockRuleID)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default BlockRules;