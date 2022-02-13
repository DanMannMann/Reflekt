using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Marsman.Reflekt.Test
{
	public interface ITreeNode { }
	public interface INodeWithSpecialThing 
	{
		string SpecialThing { get; set; }
	}
	public class Node : ITreeNode
    {
		public ITreeNode InterfaceNode { get; set; }
		public Node ClassNode { get; set; }
		public object ObjectNode { get; set; }
	}
    public class SpecialNode : Node, INodeWithSpecialThing
    {
		public string SpecialThing { get; set; } = "special 1";
	}
	public class SpecialNode2 : Node, INodeWithSpecialThing
	{
		public string SpecialThing { get; set; } = "special 2";
	}
	public class NotNode
	{
		public ITreeNode InterfaceNode { get; set; }
		public Node ClassNode { get; set; }
		public object ObjectNode { get; set; }
	}

	[TestClass]
	public class TreeEnumerableTests
	{

		private NotNode simpleTree = new NotNode
		{
			InterfaceNode = new SpecialNode
			{
				ClassNode = new SpecialNode
				{
					ObjectNode = 69
				}
			}
		};

		private NotNode tree = new NotNode
		{
			InterfaceNode = new SpecialNode
			{
				ClassNode = new SpecialNode
				{
					InterfaceNode = new Node
					{
						ObjectNode = new Node
						{
							ObjectNode = new string[] { "dave", "biihghg" }
						}
					},
					ObjectNode = 69
				},
				ObjectNode = new NotNode
				{
					InterfaceNode = new Node
					{
						InterfaceNode = new SpecialNode2
						{
							ObjectNode = "asshole"
                        }
                    }
                }
			},
			ClassNode = new Node
			{
				ClassNode = new Node
				{
					InterfaceNode = new Node
					{
						ClassNode = new Node
						{
							InterfaceNode = new SpecialNode
							{
								ClassNode = new SpecialNode
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "biihghg" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new NotNode
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new SpecialNode2
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ClassNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new SpecialNode2
										{
											ObjectNode = new string[] { "reeeg", "steve" }
										}
									},
									ObjectNode = new Node
									{
										InterfaceNode = new Node
										{
											ClassNode = new Node
											{
												InterfaceNode = new Node
												{
													ObjectNode = new Node
													{
														ObjectNode = new string[] { "dave", "guu" },
														ClassNode = new Node
                                                        {
															ObjectNode = new NotNode
															{
																InterfaceNode = new SpecialNode
																{
																	ClassNode = new SpecialNode
																	{
																		InterfaceNode = new Node
																		{
																			ObjectNode = new Node
																			{
																				ObjectNode = new string[] { "dave", "biihghg" }
																			}
																		},
																		ObjectNode = 69
																	},
																	ObjectNode = new NotNode
																	{
																		InterfaceNode = new Node
																		{
																			InterfaceNode = new SpecialNode2
																			{
																				ObjectNode = "asshole"
																			}
																		}
																	}
																},
																ClassNode = new Node
																{
																	ClassNode = new Node
																	{
																		InterfaceNode = new Node
																		{
																			ClassNode = new Node
																			{
																				InterfaceNode = new SpecialNode
																				{
																					ClassNode = new SpecialNode
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "biihghg" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new NotNode
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new SpecialNode2
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ClassNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new SpecialNode2
																							{
																								ObjectNode = new string[] { "reeeg", "steve" }
																							}
																						},
																						ObjectNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ClassNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										ObjectNode = new Node
																										{
																											ObjectNode = new string[] { "dave", "guu" }
																										}
																									},
																									ObjectNode = 69
																								},
																								ObjectNode = new NotNode
																								{
																									InterfaceNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = "asshole"
																										}
																									}
																								}
																							},
																							ClassNode = new Node
																							{
																								ClassNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										ObjectNode = new Node
																										{
																											ObjectNode = new string[] { "dave", "steve" }
																										}
																									},
																									ObjectNode = 69
																								},
																								ObjectNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = "asshole"
																										}
																									}
																								}
																							},
																							ObjectNode = "test"
																						}
																					},
																					ObjectNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ObjectNode = "test"
																			},
																			ObjectNode = new SpecialNode2
																			{
																				ObjectNode = new string[] { "reeeg", "steve" }
																			}
																		},
																		ObjectNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						ObjectNode = new Node
																						{
																							ObjectNode = new string[] { "dave", "guu" }
																						}
																					},
																					ObjectNode = 69
																				},
																				ObjectNode = new NotNode
																				{
																					InterfaceNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = "asshole"
																						}
																					}
																				}
																			},
																			ClassNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						ObjectNode = new Node
																						{
																							ObjectNode = new string[] { "dave", "steve" }
																						}
																					},
																					ObjectNode = 69
																				},
																				ObjectNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = "asshole"
																						}
																					}
																				}
																			},
																			ObjectNode = "test"
																		}
																	},
																	ObjectNode = new Node
																	{
																		InterfaceNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ObjectNode = "asshole"
																			}
																		}
																	}
																},
																ObjectNode = new NotNode
																{
																	InterfaceNode = new SpecialNode
																	{
																		ClassNode = new SpecialNode
																		{
																			InterfaceNode = new Node
																			{
																				ObjectNode = new Node
																				{
																					ObjectNode = new string[] { "dave", "biihghg" }
																				}
																			},
																			ObjectNode = 69
																		},
																		ObjectNode = new NotNode
																		{
																			InterfaceNode = new Node
																			{
																				InterfaceNode = new SpecialNode2
																				{
																					ObjectNode = "asshole"
																				}
																			}
																		}
																	},
																	ClassNode = new Node
																	{
																		ClassNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new SpecialNode
																					{
																						ClassNode = new SpecialNode
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = new Node
																								{
																									ObjectNode = new string[] { "dave", "biihghg" }
																								}
																							},
																							ObjectNode = 69
																						},
																						ObjectNode = new NotNode
																						{
																							InterfaceNode = new Node
																							{
																								InterfaceNode = new SpecialNode2
																								{
																									ObjectNode = "asshole"
																								}
																							}
																						}
																					},
																					ClassNode = new Node
																					{
																						ClassNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = new SpecialNode2
																								{
																									ObjectNode = new string[] { "reeeg", "steve" }
																								}
																							},
																							ObjectNode = new Node
																							{
																								InterfaceNode = new Node
																								{
																									ClassNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = new Node
																											{
																												ObjectNode = new string[] { "dave", "guu" }
																											}
																										},
																										ObjectNode = 69
																									},
																									ObjectNode = new NotNode
																									{
																										InterfaceNode = new Node
																										{
																											InterfaceNode = new Node
																											{
																												ObjectNode = "asshole"
																											}
																										}
																									}
																								},
																								ClassNode = new Node
																								{
																									ClassNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = new Node
																											{
																												ObjectNode = new string[] { "dave", "steve" }
																											}
																										},
																										ObjectNode = 69
																									},
																									ObjectNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											InterfaceNode = new Node
																											{
																												ObjectNode = "asshole"
																											}
																										}
																									}
																								},
																								ObjectNode = "test"
																							}
																						},
																						ObjectNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								InterfaceNode = new Node
																								{
																									ObjectNode = "asshole"
																								}
																							}
																						}
																					},
																					ObjectNode = "test"
																				},
																				ObjectNode = new SpecialNode2
																				{
																					ObjectNode = new string[] { "reeeg", "steve" }
																				}
																			},
																			ObjectNode = new Node
																			{
																				InterfaceNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "guu" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new NotNode
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ClassNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "steve" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ObjectNode = "test"
																			}
																		},
																		ObjectNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				InterfaceNode = new Node
																				{
																					ObjectNode = "asshole"
																				}
																			}
																		}
																	},
																	ObjectNode = "test"
																}
															}
														}
													}
												},
												ObjectNode = 69
											},
											ObjectNode = new NotNode
											{
												InterfaceNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = "asshole"
													}
												}
											}
										},
										ClassNode = new Node
										{
											ClassNode = new Node
											{
												InterfaceNode = new Node
												{
													ObjectNode = new Node
													{
														ObjectNode = new string[] { "dave", "steve" }
													}
												},
												ObjectNode = 69
											},
											ObjectNode = new Node
											{
												InterfaceNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = "asshole"
													}
												}
											}
										},
										ObjectNode = "test"
									}
								},
								ObjectNode = new Node
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ObjectNode = "test"
						},
						ObjectNode = new SpecialNode2
						{
							ObjectNode = new string[] { "reeeg", "steve" }
						}
					},
					ObjectNode = new Node
					{
						InterfaceNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new Node
								{
									ObjectNode = new Node
									{
										ObjectNode = new string[] { "dave", "guu" }
									}
								},
								ObjectNode = 69
							},
							ObjectNode = new NotNode
							{
								InterfaceNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = "asshole"
									}
								}
							}
						},
						ClassNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new Node
								{
									ObjectNode = new Node
									{
										ObjectNode = new string[] { "dave", "steve" }
									}
								},
								ObjectNode = 69
							},
							ObjectNode = new Node
							{
								InterfaceNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = "asshole"
									}
								}
							}
						},
						ObjectNode = "test"
					}
				},
				ObjectNode = new Node
				{
					InterfaceNode = new Node
					{
						InterfaceNode = new Node
						{
							ObjectNode = "asshole"
						}
					}
				}
			},
			ObjectNode = new NotNode
			{
				InterfaceNode = new SpecialNode
				{
					ClassNode = new SpecialNode
					{
						InterfaceNode = new Node
						{
							ObjectNode = new Node
							{
								ObjectNode = new string[] { "dave", "biihghg" }
							}
						},
						ObjectNode = 69
					},
					ObjectNode = new NotNode
					{
						InterfaceNode = new Node
						{
							InterfaceNode = new SpecialNode2
							{
								ObjectNode = "asshole"
							}
						}
					}
				},
				ClassNode = new Node
				{
					ClassNode = new Node
					{
						InterfaceNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new SpecialNode
								{
									ClassNode = new SpecialNode
									{
										InterfaceNode = new Node
										{
											ObjectNode = new Node
											{
												ObjectNode = new string[] { "dave", "biihghg" }
											}
										},
										ObjectNode = 69
									},
									ObjectNode = new NotNode
									{
										InterfaceNode = new Node
										{
											InterfaceNode = new SpecialNode2
											{
												ObjectNode = "asshole"
											}
										}
									}
								},
								ClassNode = new Node
								{
									ClassNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = new SpecialNode2
											{
												ObjectNode = new string[] { "reeeg", "steve" }
											}
										},
										ObjectNode = new Node
										{
											InterfaceNode = new Node
											{
												ClassNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = new Node
														{
															ObjectNode = new string[] { "dave", "guu" }
														}
													},
													ObjectNode = 69
												},
												ObjectNode = new NotNode
												{
													InterfaceNode = new Node
													{
														InterfaceNode = new Node
														{
															ObjectNode = "asshole"
														}
													}
												}
											},
											ClassNode = new Node
											{
												ClassNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = new Node
														{
															ObjectNode = new string[] { "dave", "steve" }
														}
													},
													ObjectNode = 69
												},
												ObjectNode = new Node
												{
													InterfaceNode = new Node
													{
														InterfaceNode = new Node
														{
															ObjectNode = "asshole"
														}
													}
												}
											},
											ObjectNode = "test"
										}
									},
									ObjectNode = new Node
									{
										InterfaceNode = new Node
										{
											InterfaceNode = new Node
											{
												ObjectNode = "asshole"
											}
										}
									}
								},
								ObjectNode = "test"
							},
							ObjectNode = new SpecialNode2
							{
								ObjectNode = new string[] { "reeeg", "steve" }
							}
						},
						ObjectNode = new Node
						{
							InterfaceNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "guu" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new NotNode
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ClassNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "steve" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new Node
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ObjectNode = "test"
						}
					},
					ObjectNode = new Node
					{
						InterfaceNode = new Node
						{
							InterfaceNode = new Node
							{
								ObjectNode = "asshole"
							}
						}
					}
				},
				ObjectNode = "test"
			}
		};

		[TestMethod]
		public void Test()
		{
			// add some loops
			(tree.ClassNode.ClassNode.ObjectNode as Node).ObjectNode = tree;

			var allBreadthContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst,
				branchStrategy: TreeBranchingStrategy.AllProperties);

			var l1 = allBreadthContextResult.ToList();
			var l2 = allBreadthContextResult.ToList();


			Array s;
			var specialsResult = simpleTree.AsContextualTreeEnumerable<INodeWithSpecialThing>(
				branchStrategy: TreeBranchingStrategy.AllProperties).ToList();
			var specialsResult3 = tree.AsContextualTreeEnumerable<INodeWithSpecialThing>(
				branchStrategy: TreeBranchingStrategy.AllProperties, enumStrategy: TreeEnumerationStrategy.BreadthFirst).ToList();
			var specialsResult4 = tree.AsContextualTreeEnumerable<INodeWithSpecialThing>(
				branchStrategy: TreeBranchingStrategy.AllProperties, enumStrategy: TreeEnumerationStrategy.DepthFirst).ToList();

			var propTypeResult = tree.AsTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.PropertyTypeIsValueType).ToList();
			var propTypeContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.PropertyTypeIsValueType).ToList();

			var propValueResult = tree.AsTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.PropertyValueIsValueType).ToList();
			var propValueContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.PropertyValueIsValueType).ToList();

			var allResult = tree.AsTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.AllProperties).ToList();
			var allContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				branchStrategy: TreeBranchingStrategy.AllProperties).ToList();


			var propTypeBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchStrategy: TreeBranchingStrategy.PropertyTypeIsValueType).ToList();
			var propTypeBreadthContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchStrategy: TreeBranchingStrategy.PropertyTypeIsValueType).ToList();

			var propValueBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchStrategy: TreeBranchingStrategy.PropertyValueIsValueType).ToList();
			var propValueBreadthContextResult = tree.AsContextualTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchStrategy: TreeBranchingStrategy.PropertyValueIsValueType).ToList();

			var allBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchStrategy: TreeBranchingStrategy.AllProperties).ToList();
		}
	}
}
