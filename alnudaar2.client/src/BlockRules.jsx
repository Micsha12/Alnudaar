import React, { useState, useEffect } from 'react';
import './BlockRules.css';
import { useAuth } from './AuthContext';

function BlockRules() {
    const { auth, selectedDevice } = useAuth();
    const [rules, setRules] = useState([]);
    const [newRule, setNewRule] = useState({ type: 'website', value: '', timeRange: '' });
    const [editingRule, setEditingRule] = useState(null); // Track the rule being edited

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

    const handleAddOrUpdateRule = async () => {
        if (!selectedDevice || !selectedDevice.deviceID) {
            console.error('No device selected');
            return;
        }
    
        const ruleToSave = {
            ...newRule,
            userID: auth.user.userID,
            deviceID: selectedDevice.deviceID,
            blockRuleID: editingRule ? editingRule.blockRuleID : undefined, // Include BlockRuleID if editing
        };
    
        if (editingRule) {
            // Update the rule
            const response = await fetch(`https://localhost:7200/api/blockrules/${editingRule.blockRuleID}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(ruleToSave),
            });
    
            if (response.ok) {
                const updatedRule = await response.json();
                setRules(rules.map((rule) => (rule.blockRuleID === updatedRule.blockRuleID ? updatedRule : rule)));
                setEditingRule(null); // Exit editing mode
            } else {
                console.error('Failed to update blocking rule');
            }
        } else {
            // Create a new rule
            const response = await fetch('https://localhost:7200/api/blockrules', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(ruleToSave),
            });
    
            if (response.ok) {
                const addedRule = await response.json();
                setRules([...rules, addedRule]);
            } else {
                console.error('Failed to add blocking rule');
            }
        }
    
        // Reset the form
        setNewRule({ type: 'website', value: '', timeRange: '' });
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

    const handleEditRule = (rule) => {
        setEditingRule(rule); // Set the rule being edited
        setNewRule({
            type: rule.type,
            value: rule.value,
            timeRange: rule.timeRange,
            deviceID: rule.deviceID, // Include DeviceID
        });
    };

    const handleAllDayToggle = (e) => {
        if (e.target.checked) {
            setNewRule({ ...newRule, timeRange: '00:00-23:59' }); // Set time range to "All Day"
        } else {
            setNewRule({ ...newRule, timeRange: '' }); // Clear time range
        }
    };

    return (
        <div className="block-rules-container">
            <h1>Manage Blocking Rules</h1>
            
            {/* Form for Adding or Editing Rules */}
            <div className="add-rule">
                <h2>{editingRule ? 'Edit Rule' : 'Add New Rule'}</h2>
                <div className="form-group">
                    <label htmlFor="rule-type">Rule Type:</label>
                    <select
                        id="rule-type"
                        value={newRule.type}
                        onChange={(e) => setNewRule({ ...newRule, type: e.target.value })}
                    >
                        <option value="website">Website</option>
                        <option value="application">Application</option>
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="rule-value">Website or Application:</label>
                    <input
                        id="rule-value"
                        type="text"
                        placeholder="Enter website or application name"
                        value={newRule.value}
                        onChange={(e) => setNewRule({ ...newRule, value: e.target.value })}
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="time-range">Time Range:</label>
                    <input
                        id="time-range"
                        type="text"
                        placeholder="e.g., 09:00-17:00"
                        value={newRule.timeRange}
                        onChange={(e) => setNewRule({ ...newRule, timeRange: e.target.value })}
                        disabled={newRule.timeRange === '00:00-23:59'} // Disable if "All Day" is checked
                    />
                </div>
                <div className="form-group">
                    <label>
                        <input
                            type="checkbox"
                            checked={newRule.timeRange === '00:00-23:59'}
                            onChange={handleAllDayToggle}
                        />
                        All Day
                    </label>
                </div>
                <button className="submit-button" onClick={handleAddOrUpdateRule}>
                    {editingRule ? 'Update Rule' : 'Add Rule'}
                </button>
            </div>
    
            {/* List of Existing Rules */}
            <div className="rules-list">
                <h2>Existing Rules</h2>
                {rules.length > 0 ? (
                    <ul>
                        {rules.map((rule) => (
                            <li key={rule.blockRuleID} className="rule-item">
                                <span>
                                    <strong>{rule.type === 'website' ? 'Website' : 'Application'}:</strong> {rule.value} 
                                    <em> ({rule.timeRange})</em>
                                </span>
                                <div className="rule-actions">
                                    <button onClick={() => handleEditRule(rule)}>Edit</button>
                                    <button onClick={() => handleDeleteRule(rule.blockRuleID)}>Delete</button>
                                </div>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <p>No rules added yet.</p>
                )}
            </div>
        </div>
    );
}
export default BlockRules;